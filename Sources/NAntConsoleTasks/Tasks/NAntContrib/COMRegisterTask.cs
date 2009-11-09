using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;
using NAnt.Core.Tasks;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.NAntContrib
{
    [TaskName("com-register")]
    public class COMRegisterTask : ExternalProgramBase
    {
        private FileInfo _file;
        /// <summary>
        /// The name of the file to register.
        /// </summary>
        [TaskAttribute("file")]
        public FileInfo File
        {
            get { return _file; }
            set { _file = value; }
        }

        private bool _unregister;
        /// <summary>Unregistering this time. ( /u paramater )Default is "false".</summary>
        [TaskAttribute("unregister")]
        [BooleanValidator()]
        public bool Unregister
        {
            get { return _unregister; }
            set { _unregister = value; }
        }

        public override string ExeName
        {
            get
            {
                return "regsvr32";
            }
        }

        public override string ProgramArguments
        {
            get
            {
                StringBuilder sbArgs = new StringBuilder(" /s");

                if (Unregister)
                    sbArgs.Append(" /u");

                sbArgs.AppendFormat(" \"{0}\"", File.FullName);

                return sbArgs.ToString();
            }
        }

        protected override void ExecuteTask()
        {
            if (Unregister)
                Log(Level.Info, Resources.ComUnregister, File.FullName);
            else
                Log(Level.Info, Resources.ComRegister, File.FullName);

            base.ExecuteTask();
        }
    }
}
