using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.NAntContrib;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Net;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite
{
    [TaskName("replace-gac-assembly")]
    public class ReplaceGacAssemblyTask : BaseCompositeTask
    {
        private FileInfo source;
        [TaskAttribute("source", Required = true)]
        public FileInfo Source
        {
            get { return source; }
            set { source = value; }
        }

        private FileInfo dest;
        [TaskAttribute("dest", Required = true)]
        public FileInfo Dest
        {
            get { return dest; }
            set { dest = value; }
        }

        protected override void ExecuteTask()
        {
            if (!Source.Exists)
            {
                throw new BuildException(string.Format(Resources.ReplaceComDllSourceError, Source.FullName));
            }

            if (Dest.Exists)
            {
                KillProcessTask killProcessTask = CreateTask<KillProcessTask>();
                killProcessTask.Dll = Dest;
                killProcessTask.Execute();

                GacUninstallTask gacuninstallTask = CreateTask<GacUninstallTask>();
                gacuninstallTask.Assembly = Dest;
                gacuninstallTask.Execute();
            }

            CopyTask copyTask = CreateTask<CopyTask>();
            copyTask.SourceFile = Source;
            copyTask.ToFile = Dest;
            copyTask.Execute();

            GacInstallTask gacInstallTask = CreateTask<GacInstallTask>();
            gacInstallTask.Assembly = Dest;
            gacInstallTask.Execute();
        }
    }
}
