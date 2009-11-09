using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using SharpSvn;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.SVN
{
    [TaskName("svn-checkout")]
    public class SVNCheckoutTask : BaseSVNTask
    {
        private DirectoryInfo dir;
        [TaskAttribute("dir", Required=true)]
        public DirectoryInfo Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        private Uri url;
        [TaskAttribute("url", Required = true)]
        public Uri Url
        {
            get { return url; }
            set { url = value; }
        }

        protected override void ExecuteSVNTask(SvnClient client)
        {
            if (Dir.Exists && !IsEmpty(Dir))
            {
                throw new BuildException(string.Format(Resources.SVNCheckoutEmptyDirectory, Dir.FullName), Location);
            }

            Log(Level.Info, Resources.SVNCheckingOut, Url, Dir.FullName);
            SvnCheckOutArgs args = new SvnCheckOutArgs();
            args.ThrowOnError = true;
            args.Depth = SvnDepth.Infinity;
            args.Revision = SvnRevision.Head;
            SvnUpdateResult result;

            bool conflictedFiles = false;
            client.Conflict += delegate(object sender, SvnConflictEventArgs conflictArgs)
                                    {
                                        conflictedFiles = true;
                                        Log(Level.Warning, string.Concat(@"Conflicted: ", conflictArgs.Path));
                                    };

            client.Notify += delegate(object sender, SvnNotifyEventArgs notifyArgs)
                                    {
                                        Log(Level.Info, string.Concat(notifyArgs.Action, ": ", notifyArgs.Path));
                                    };

            client.CheckOut(SvnUriTarget.FromUri(Url), Dir.FullName, args, out result);
            
            if (conflictedFiles)
            {
                throw new BuildException(string.Format(Resources.SVNConflict, Dir.FullName));
            }

            if (result != null)
            {
                Log(Level.Info, Resources.SVNCheckedOut, Dir.FullName, result.Revision);
            }
        }

        private static bool IsEmpty(DirectoryInfo dir)
        {
            if (!dir.Exists)
                return true;

            return ((dir.GetDirectories().Length == 0) && (dir.GetFiles().Length == 0));
        }
    }
}
