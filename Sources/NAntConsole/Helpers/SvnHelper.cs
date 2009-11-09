using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using CDS.Framework.Tools.NAntConsole.Configuration;
using SharpSvn;
using Resources=CDS.Framework.Tools.NAntConsole.UI.Resources;

namespace CDS.Framework.Tools.NAntConsole.Helpers
{
    internal static class SvnHelper
    {
        static readonly Regex reTrunkSubTagOrSubBranchOnly = new Regex(@"^(?<leading>.+/)(trunk|branches/[^/]+|tags/[^/]+)/?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        static readonly Regex reStripTagsTrunkBranches = new Regex(@"(?<leading>.+)/(trunk|(branches/[^/]+)|(tags/[^/]+))(?<ending>/?.*)", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        public const string BRANCHED_AT_PROPERTY_NAME = @"nc:branchedat";
        public const string BRANCHED_FROM_PROPERTY_NAME = @"nc:branchedfrom";
        public const string EXTERNALS_PROPERTY_NAME = @"svn:externals";

        public static Collection<SvnInfoEventArgs> GetChildrenInfo(string uri)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                SvnInfoArgs args = new SvnInfoArgs();
                args.Depth = SvnDepth.Children;
                args.ThrowOnError = false;
                Collection<SvnInfoEventArgs> svnGetInfoResult;
                svnClient.GetInfo(SvnTarget.FromString(uri), args, out svnGetInfoResult);
                
                if(svnGetInfoResult.Count > 0)
                    svnGetInfoResult.RemoveAt(0);
                
                return SortResult(svnGetInfoResult);
            }
        }

        public static string GetPathFromSvnUri(string svnUriWithoutRepository)
        {
            StringBuilder sbResult = new StringBuilder();
            string[] splittedUri = svnUriWithoutRepository.Split('/');
            bool first = true;
            foreach (string uriPart in splittedUri)
            {
                if (!string.IsNullOrEmpty(uriPart))
                {
                    if (first)
                    {
                        sbResult.Append(uriPart);
                        first = false;
                    }
                    else
                    {
                        sbResult.AppendFormat("\\{0}", uriPart);
                    }

                }
            }
            return sbResult.ToString();
        }

        public static void CheckOut(string svnUri, string localDirectory, OnSvnExecutionProgress executionProgress)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                svnClient.Notify += delegate(object sender, SvnNotifyEventArgs e)
                                        {
                                            InvokeExecutionProgress(e, executionProgress);
                                        };

                svnClient.CheckOut(SvnUriTarget.FromString(svnUri), localDirectory);
            }
        }

        public static void Update(string path, OnSvnExecutionProgress executionProgress)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                SvnUpdateArgs args = new SvnUpdateArgs();
                args.ThrowOnError = true;
                args.Depth = SvnDepth.Infinity;
                args.Revision = SvnRevision.Head;
                svnClient.Conflict += delegate(object sender, SvnConflictEventArgs e)
                                        {
                                            InvokeExecutionProgress(e, executionProgress);
                                        };

                svnClient.Notify += delegate(object sender, SvnNotifyEventArgs e)
                                        {
                                            InvokeExecutionProgress(e, executionProgress);
                                        };
                svnClient.Update(path, args);
            }
        }

        public static string CreateFolder(string svnUri, string folderName)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                Uri targetUri = new Uri(new Uri(svnUri), folderName);
                SvnCreateDirectoryArgs args = new SvnCreateDirectoryArgs();
                args.LogMessage = string.Format(Resources.SvnCreateDirectoryLogMessage, targetUri);
                svnClient.RemoteCreateDirectory(targetUri, args);
                return targetUri.ToString();
            }
        }

        public static void AddFolder(string path)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                svnClient.Add(path);
            }
        }

        public static string GetProperty(string target, string propertyName)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                string result;
                svnClient.TryGetProperty(SvnTarget.FromString(target), propertyName, out result);
                return result;
            }
        }

        public static void SetProperty(string path, string propertyName, string value)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                svnClient.SetProperty(path, propertyName, value);
            }
        }

        public static void DeleteProperty(string path, string propertyName)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                svnClient.DeleteProperty(path, propertyName);
            }
        }

        public static string GetNewBranchLeadingName(string svnUri)
        {
            Match mLeading = reTrunkSubTagOrSubBranchOnly.Match(svnUri);
            if (!mLeading.Success)
            {
                throw new ApplicationException("Error in selected svn uri - enabled to determine leading branch.");
            }

            string leading = mLeading.Groups["leading"].Value;
            using (SvnClient svnClient = new SvnClient())
            {
                Collection<SvnInfoEventArgs> childrenInfo = GetChildrenInfo(leading);
                foreach (SvnInfoEventArgs infoEventArgs in childrenInfo)
                {
                    if (infoEventArgs.Path.Equals("branches", StringComparison.InvariantCultureIgnoreCase))
                    {
                        leading = infoEventArgs.Uri.ToString();
                    }
                }
            }
            return leading;
        }

        public static long RemoteBranch(string source, string dest)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                Uri targetUri = new Uri(dest);
                SvnCopyArgs args = new SvnCopyArgs();
                args.LogMessage = string.Format(Resources.SvnLogCreatingBranch, dest);
                svnClient.RemoteCopy(SvnTarget.FromString(source), targetUri, args);
                long branchRevisionNumber = 0;
                svnClient.Info(SvnTarget.FromUri(targetUri), new EventHandler<SvnInfoEventArgs>(delegate(object sender, SvnInfoEventArgs infoArgs)
                {
                    branchRevisionNumber = infoArgs.Revision;
                }));

                SvnSetPropertyArgs setPropertyArgsAt = new SvnSetPropertyArgs();
                setPropertyArgsAt.BaseRevision = branchRevisionNumber;
                setPropertyArgsAt.LogMessage = string.Format(Resources.SvnLogSetProperty, BRANCHED_AT_PROPERTY_NAME, branchRevisionNumber);
                svnClient.SetProperty(targetUri, BRANCHED_AT_PROPERTY_NAME, branchRevisionNumber.ToString(), setPropertyArgsAt);

               /* SvnSetPropertyArgs setPropertyArgsFrom = new SvnSetPropertyArgs();
                setPropertyArgsFrom.BaseRevision = branchRevisionNumber;
                setPropertyArgsFrom.LogMessage = string.Format(Resources.SvnLogSetProperty, BRANCHED_FROM_PROPERTY_NAME, source);
                svnClient.SetProperty(targetUri, BRANCHED_FROM_PROPERTY_NAME, source, setPropertyArgsFrom);*/

                return branchRevisionNumber;
            }
        }

        public static MergeBranchInfo GetMergeBranchInfo(string svnUri)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                try
                {
                    string revValue;
                    string uriValue;
                    svnClient.GetProperty(SvnTarget.FromString(svnUri), BRANCHED_AT_PROPERTY_NAME, out revValue);
                    svnClient.GetProperty(SvnTarget.FromString(svnUri), BRANCHED_FROM_PROPERTY_NAME, out uriValue);
                    if (string.IsNullOrEmpty(revValue) || string.IsNullOrEmpty(uriValue))
                    {
                        throw new ApplicationException(Resources.MissingBranchProperties);
                    }
                    else
                    {
                        return new MergeBranchInfo(Convert.ToInt32(revValue), uriValue);
                    }
                }
                catch
                {
                    throw new ApplicationException(Resources.MissingBranchProperties);
                }
            }
        }

        public static bool Merge(string targetPath, string sourceUri, int startRevision)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                bool conflicts = false;
                SvnRevisionRange revRange = new SvnRevisionRange(new SvnRevision(startRevision), SvnRevision.Head);
                SvnMergeArgs mergeArgs = new SvnMergeArgs();
                svnClient.Conflict += delegate(object sender, SvnConflictEventArgs e)
                                          {
                                              conflicts = true;
                                          };
                svnClient.Merge(targetPath, SvnTarget.FromString(sourceUri), revRange);
                return conflicts;
            }
        }

        public static void Commit(string path, string message)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                SvnCommitArgs commitArgs = new SvnCommitArgs();
                commitArgs.LogMessage = message;
                svnClient.Commit(path, commitArgs);
            }
        }

        public static bool HasPendingModifications(string path)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                Collection<SvnStatusEventArgs> status;
                svnClient.GetStatus(path, out status);
                return (status.Count != 0);
            }
        }

        public static bool IsLocalFolderUnderSvnControl(string path)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                return svnClient.GetUriFromWorkingCopy(path) != null;
            }
        }

        public static bool IsUriTrunkTagBranch(string svnUri)
        {
            return reTrunkSubTagOrSubBranchOnly.Match(svnUri).Success;
        }

        public static string GetUriFromWorkingCopy(string path)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                Uri svnUri = svnClient.GetUriFromWorkingCopy(path);
                return svnUri != null ? svnUri.ToString() : null;
            }
        }

        public static string TryGuessRepositoryFromWorkingCopy(string path)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                Uri svnUri = svnClient.GetUriFromWorkingCopy(path);
                if (svnUri == null)
                {
                    return null;
                }

                NAntConsoleConfigurationSection configurationSection =
                    NAntConsoleConfigurationSection.GetConfigurationSection();
                foreach (SvnRepositoryElement repository in configurationSection.SvnRepositories)
                {
                    if (svnUri.ToString().Contains(repository.Uri))
                    {
                        return repository.Uri;
                    }
                }
                return svnUri.ToString();
            }
        }

        public static string StripTagsTrunkBranches(string value)
        {
            Match mStripTagsTrunkBranches = reStripTagsTrunkBranches.Match(value);
            if (!mStripTagsTrunkBranches.Success)
            {
                return value;
            } else
            {
                return reStripTagsTrunkBranches.Replace(value, "${leading}${ending}");
            }
        }

        public static bool AreUpdatesAvalaible(string path)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                bool result = false;
                SvnStatusArgs args = new SvnStatusArgs();
                args.Depth = SvnDepth.Infinity;
                args.RetrieveRemoteStatus = true;
                svnClient.Status(path, args, delegate(object sender, SvnStatusEventArgs eventArgs)
                                                 {
                                                     if(eventArgs.IsRemoteUpdated)
                                                         result = true;
                                                 });
                return result;
            }
        }

        private static void InvokeExecutionProgress(SvnNotifyEventArgs notifyEventArgs, OnSvnExecutionProgress executionProgress)
        {
            if (executionProgress != null)
            {
                executionProgress(new SvnExecutionProgressEventArgs(string.Concat(notifyEventArgs.Action.ToString(), " : ", notifyEventArgs.Path)));
            }
        }

        private static void InvokeExecutionProgress(SvnConflictEventArgs notifyEventArgs, OnSvnExecutionProgress executionProgress)
        {
            if (executionProgress != null)
            {
                executionProgress(new SvnExecutionProgressEventArgs(string.Concat("Conflicted: ", notifyEventArgs.Path)));
            }
        }

        private static Collection<SvnInfoEventArgs> SortResult(IEnumerable<SvnInfoEventArgs> svnGetInfoResult)
        {
            List<SvnInfoEventArgs> tempList = new List<SvnInfoEventArgs>(svnGetInfoResult);
            tempList.Sort(new SvnInfoEventArgsComparer());
            return new Collection<SvnInfoEventArgs>(tempList);
        }

        class SvnInfoEventArgsComparer : IComparer<SvnInfoEventArgs>
        {
            public int Compare(SvnInfoEventArgs x, SvnInfoEventArgs y)
            {
                return x.Path.CompareTo(y.Path);
            }
        }

        public delegate void OnSvnExecutionProgress(SvnExecutionProgressEventArgs args);
    }

    public class MergeBranchInfo
    {
        private readonly int revision;
        private readonly string fromUri;

        public MergeBranchInfo(int revision, string fromUri)
        {
            this.revision = revision;
            this.fromUri = fromUri;
        }

        public string FromUri
        {
            get { return fromUri; }
        }

        public int Revision
        {
            get { return revision; }
        }
    }
}
