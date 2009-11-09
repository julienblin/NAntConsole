using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.NAntContrib;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite
{
    [TaskName("replace-com-dll")]
    public class ReplaceComDllTask : BaseCompositeTask
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

                COMRegisterTask comUnregisterTask = CreateTask<COMRegisterTask>();
                comUnregisterTask.Unregister = true;
                comUnregisterTask.File = Dest;
                comUnregisterTask.Execute();
            }

            CopyTask copyTask = CreateTask<CopyTask>();
            copyTask.SourceFile = Source;
            copyTask.ToFile = Dest;
            copyTask.Execute();

            COMRegisterTask comRegisterTask = CreateTask<COMRegisterTask>();
            comRegisterTask.File = Dest;
            comRegisterTask.Execute();
        }
    }
}
