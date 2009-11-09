using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using SharpSvn;
using System.Diagnostics;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.SVN
{
    [TaskName("svn-commit")]
    public class SVNCommitTask : BaseSVNMessageTask
    {
        private DirectoryInfo dir;
        [TaskAttribute("dir")]
        public DirectoryInfo Dir
        {
            get { return dir; }
            set { dir = value; }
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

            string sourcesFolder = string.Concat(Dir.FullName, @"\Sources");

            if (Directory.Exists(sourcesFolder))
            {
                Log(Level.Info, Resources.SVNAdding, Dir.FullName);
                SvnAddArgs addArgs = new SvnAddArgs();
                addArgs.Depth = SvnDepth.Infinity;
                addArgs.Force = true;
                client.Add(sourcesFolder, addArgs);
            }
            else
            {
                Log(Level.Info, Resources.SVNSourcesFolderNotFound, sourcesFolder);
            }


            Log(Level.Info, Resources.SVNCommitCommitting, Dir.FullName, Message);
            SvnCommitArgs args = new SvnCommitArgs();
            args.LogMessage = Message;
            args.ThrowOnError = true;
            SvnCommitResult result;
            client.Commit(Dir.FullName, args, out result);
            if (result != null)
            {
                Log(Level.Info, Resources.SVNCommitResult, Dir.FullName, result.Revision, result.Author);
            }
        }
    }
}
