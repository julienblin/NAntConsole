using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core.Attributes;
using NAnt.Core;
using NAnt.Core.Tasks;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Functions;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows;
using System.IO;
using System.Reflection;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.COM
{
    [TaskName("dcomperm")]
    public class DCOMPermTask : ExternalProgramBase
    {
        private string appId;
        [TaskAttribute("appid", Required=true)]
        [StringValidator(AllowEmpty=false)]
        public string AppId
        {
            get { return appId; }
            set { appId = value; }
        }

        private DCOMPermOptions option = DCOMPermOptions.runas;
        [TaskAttribute("option")]
        public DCOMPermOptions Option
        {
            get { return option; }
            set { option = value; }
        }

        private string username;
        [TaskAttribute("username")]
        [StringValidator(AllowEmpty=false)]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string password;
        [TaskAttribute("password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public override string ExeName
        {
            get
            {
                return @"dcomperm.exe";
            }
            set
            {
                // no-op
            }
        }

        public override string ProgramArguments
        {
            get {
                switch (Option) {
                    case DCOMPermOptions.runas:
                        return string.Format("-{0} {{{1}}} \"{2}\" \"{3}\"", Option, AppId, Username, Password);
                    case DCOMPermOptions.localAccess:
                        return string.Format("-aa {{{0}}} set \"{1}\" permit level:l", AppId, Username);
                    case DCOMPermOptions.localLaunch:
                        return string.Format("-al {{{0}}} set \"{1}\" permit level:l", AppId, Username);
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        protected override void ExecuteTask()
        {
            Log(Level.Info, "Executing DCOMPerm with AppId {0} and option {1}...", AppId, Option);
            CheckVCExecReditPackage();
            base.ExecuteTask();
        }

        private void CheckVCExecReditPackage()
        {
            if (!MsiFunctions.IsProductInstalled("F37207D363F3FBE43901D6914195B624"))
            {
                Log(Level.Info, "Installing VC++ 2008 runtime...");
                ExecTask execTask = new ExecTask();
                CopyTo(execTask);
                execTask.Parent = this;

                string workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string vcredistPath = Path.Combine(workingDirectory, "vcredist_x86.exe");
                execTask.FileName = vcredistPath;
                execTask.Execute();
            }
        }
    }
}

public enum DCOMPermOptions
{
    runas,
    localAccess,
    localLaunch
}


