using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using SharpSvn;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.SVN
{
    [TaskName("svn-update")]
    public class SVNUpdateTask : BaseSVNTask
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

            Log(Level.Info, Resources.SVNUpdateUpdating, Dir.FullName);
            SvnUpdateArgs args = new SvnUpdateArgs();
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

            client.Update(Dir.FullName, args, out result);

            if (conflictedFiles)
            {
                throw new BuildException(string.Format(Resources.SVNConflict, Dir.FullName));
            }

            if (result != null)
            {
                Log(Level.Info, Resources.SVNUpdateResult, Dir.FullName, result.Revision);
            }
        }
    }
}
