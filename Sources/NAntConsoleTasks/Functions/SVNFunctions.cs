using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NAnt.Core;
using NAnt.Core.Attributes;
using SharpSvn;
using System.Collections.ObjectModel;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Functions
{
    [FunctionSet("svn", "svn")]
    class SVNFunctions : FunctionSetBase
    {
        static readonly Regex reSvnUriStripTrunkOrBranches = new Regex(@"^(?<baseUri>.+)/(trunk|branches)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        public SVNFunctions(Project project, PropertyDictionary properties)
            : base(project, properties)
        {
        }

        [Function("get-uri-from-path")]
        public string GetUriFromPath()
        {
            return GetUriFromPath(Project.BaseDirectory);
        }

        [Function("get-uri-from-path")]
        public string GetUriFromPath(string path)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                return svnClient.GetUriFromWorkingCopy(path).ToString();
            }
        }

        [Function("get-tags-uri-from-path")]
        public string GetTagsUriFromPath()
        {
            return GetTagsUriFromPath(Project.BaseDirectory);
        }

        [Function("get-tags-uri-from-path")]
        public string GetTagsUriFromPath(string path)
        {
            string uri = GetUriFromPath(path);
            Match mSvnUriStripTrunkOrBranches = reSvnUriStripTrunkOrBranches.Match(uri);
            if (mSvnUriStripTrunkOrBranches.Success)
            {
                return string.Format("{0}/tags", mSvnUriStripTrunkOrBranches.Groups["baseUri"].Value);
            }
            else
            {
                throw new BuildException(string.Format(Resources.SVNFunctionsBadBaseUriForTags, uri));
            }
        }
		
		[Function("has-pending-modifications")]
        public bool HasPendingModifications()
        {
            return HasPendingModifications(Project.BaseDirectory);
        }
		
        [Function("has-pending-modifications")]
        public bool HasPendingModifications(string path)
        {
            using (SvnClient svnClient = new SvnClient())
            {
                Collection<SvnStatusEventArgs> status;
                svnClient.GetStatus(path, out status);
                return (status.Count != 0);
            }
        }
    }
}
