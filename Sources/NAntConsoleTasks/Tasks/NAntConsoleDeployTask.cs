using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core.Attributes;
using NAnt.Core;
using System.IO;
using NAnt.Core.Tasks;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks
{
    [TaskName("nantconsole-deploy")]
    public class NAntConsoleDeployTask : ExternalProgramBase
    {
        const string NANTCONSOLE_EXENAME = @"CDS.Framework.Tools.NAntConsole.exe";

        private FileInfo deployfile;
        [TaskAttribute("deploy-file", Required=true)]
        public FileInfo Deployfile
        {
            get { return deployfile; }
            set { deployfile = value; }
        }

        private string target = @"install";
        [TaskAttribute("target")]
        [StringValidator(AllowEmpty=false)]
        public string Target
        {
            get { return target; }
            set { target = value; }
        }

        public override string ProgramFileName
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NANTCONSOLE_EXENAME);
            }
        }

        public override string ProgramArguments
        {
            get
            {
                StringBuilder sbArgs = new StringBuilder();
                sbArgs.AppendFormat(" \"{0}\"", Deployfile.FullName);
                sbArgs.AppendFormat(" \"{0}\"", Target);
                sbArgs.Append(" /y");

                return sbArgs.ToString();
            }
        }
    }
}
