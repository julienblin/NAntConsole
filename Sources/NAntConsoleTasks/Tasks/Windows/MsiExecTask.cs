using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;
using System.IO;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows
{
    [TaskName("msiexec")]
    public class MsiExecTask : ExternalProgramBase
    {
        const string EXE_NAME = @"msiexec.exe";

        private FileInfo msi;
        [TaskAttribute("msi", Required=true)]
        public FileInfo Msi
        {
            get { return msi; }
            set { msi = value; }
        }

        private MsiExecTaskAction action = MsiExecTaskAction.install;
        [TaskAttribute("action")]
        public MsiExecTaskAction Action
        {
            get { return action; }
            set { action = value; }
        }

        private bool quiet = true;
        [TaskAttribute("quiet")]
        public bool Quiet
        {
            get { return quiet; }
            set { quiet = value; }
        }

        private bool passive = true;
        [TaskAttribute("passive")]
        public bool Passive
        {
            get { return passive; }
            set { passive = value; }
        }

        protected override void ExecuteTask()
        {
            Log(Level.Info, Resources.MsiExecAction, action, msi);
            base.ExecuteTask();
        }

        public override string ProgramArguments
        {
            get
            {
                StringBuilder sbArgs = new StringBuilder();
                if (quiet)
                    sbArgs.Append("/quiet ");

                if (passive)
                    sbArgs.Append("/passive ");

                switch (action)
                {
                    case MsiExecTaskAction.install:
                        sbArgs.AppendFormat("/i \"{0}\" ", msi.FullName);
                        break;
                    case MsiExecTaskAction.administrative:
                        sbArgs.AppendFormat("/a \"{0}\" ", msi.FullName);
                        break;
                    case MsiExecTaskAction.uninstall:
                        sbArgs.AppendFormat("/x \"{0}\" ", msi.FullName);
                        break;
                    default:
                        break;
                }

                return sbArgs.ToString();
            }
        }

        public override string ExeName
        {
            get
            {
                return EXE_NAME;
            }
            set
            {
                ;
            }
        }
    }

    public enum MsiExecTaskAction
    {
        install,
        administrative,
        uninstall
    }
}
