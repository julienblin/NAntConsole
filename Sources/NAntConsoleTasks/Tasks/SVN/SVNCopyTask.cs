using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Functions;
using NAnt.Core;
using NAnt.Core.Attributes;
using SharpSvn;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.SVN
{
    [TaskName("svn-copy")]
    public class SVNCopyTask : BaseSVNMessageTask
    {
        private DirectoryInfo dir;
        [TaskAttribute("dir")]
        public DirectoryInfo Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        private Uri svnUri;
        [TaskAttribute("uri", Required = true)]
        public Uri SvnUri
        {
            get { return svnUri; }
            set { svnUri = value; }
        }

        protected override void ExecuteSVNTask(SvnClient client)
        {
            if (Dir == null)
            {
                Dir = new DirectoryInfo(Project.BaseDirectory);
            }

            if (!Dir.Exists)
            {
                throw new BuildException(string.Format(Resources.MissingDirectory, Dir.FullName), Location);
            }

            SVNFunctions svnFunctions = new SVNFunctions(Project, Properties);
            string sourceUri = svnFunctions.GetUriFromPath(Dir.FullName);
            Log(Level.Info, Resources.SVNCopyCopying, sourceUri, SvnUri);

            SvnCopyArgs args = new SvnCopyArgs();
            args.ThrowOnError = true;
            args.LogMessage = Message;

            SvnCommitResult result;
            client.RemoteCopy(SvnTarget.FromString(sourceUri), SvnUri, args, out result);

            if (result != null)
            {
                Log(Level.Info, Resources.SVNCopyResult, Dir.FullName, SvnUri, result.Revision, result.Author);
            }
        }
    }
}
