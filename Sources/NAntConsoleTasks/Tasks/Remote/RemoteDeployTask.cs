using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;
using NAnt.Core.Types;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Remote
{
    [TaskName("remote-deploy")]
    public class RemoteDeployTask : Task
    {
        static readonly Regex reInstallDir = new Regex(@"InstallDir\s+REG_SZ\s+(?<name>[^\n\r]+)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        private string machine;
        [TaskAttribute("machine", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string Machine
        {
            get { return machine; }
            set { machine = value; }
        }

        private string username;
        [TaskAttribute("username", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string password;
        [TaskAttribute("password", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string package;
        [TaskAttribute("package", Required=true)]
        [StringValidator(AllowEmpty=false)]
        public string Package
        {
            get { return package; }
            set { package = value; }
        }
	
        protected override void ExecuteTask()
        {
            Log(Level.Info, "Connecting to {0}...", Machine);
            string nantConsoleRemoteInstallDir = GetRemoteInstallDir();
            Log(Level.Info, "Remote NAntConsole installation found : {0}", nantConsoleRemoteInstallDir);
            DeployPackage(nantConsoleRemoteInstallDir);
        }

        private void DeployPackage(string nantConsoleRemoteInstallDir)
        {
            string cmdLineExePath = Path.Combine(nantConsoleRemoteInstallDir, "CDS.Framework.Tools.NAntConsoleCmdLine.exe");
            string cmdLine = string.Format("\"{0}\" \"{1}\" install", cmdLineExePath, Package);
            Log(Level.Info, "Executing {0}", cmdLine);
            Log(Level.Info, ExecuteViaPsexec(cmdLine, nantConsoleRemoteInstallDir));
        }

        private string GetRemoteInstallDir()
        {
            string commandLine = string.Format("reg query \"HKLM\\{0}\" /v \"{1}\"",
                RemoteConstants.NANT_CONSOLE_INSTALL_FOLDER_REG_KEY,
                RemoteConstants.NANT_CONSOLE_INSTALL_FOLDER_REG_VALUE
            );

            string commandOutput = ExecuteViaPsexec(commandLine, null);

            Match mInstallDir = reInstallDir.Match(commandOutput);
            if (mInstallDir.Success)
            {
                return mInstallDir.Groups["name"].Value;
            }
            else
            {
                throw new BuildException("Enable to retreive NAntConsole remote installation directory.");
            }
        }

        private string ExecuteViaPsexec(string commandLine, string remoteWorkingDirectory)
        {
            string workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string psexecPath = Path.Combine(workingDirectory, "psexec.exe");
            string tempOutputFile = Path.Combine(workingDirectory, Path.GetRandomFileName());
            string tempBatFile = Path.Combine(workingDirectory, String.Concat(Path.GetFileNameWithoutExtension(Path.GetRandomFileName()), ".bat"));
            string arguments = null;
            if (string.IsNullOrEmpty(remoteWorkingDirectory))
            {
                arguments = string.Format("\\\\{0} -u \"{1}\" -p \"{2}\" {3} > \"{4}\"",
                    Machine,
                    Username,
                    Password,
                    commandLine,
                    tempOutputFile
                );
            }
            else
            {
                arguments = string.Format("\\\\{0} -u \"{1}\" -p \"{2}\" -w \"{3}\" {4} > \"{5}\"",
                    Machine,
                    Username,
                    Password,
                    remoteWorkingDirectory,
                    commandLine,
                    tempOutputFile
                );
            }

            //Log(Level.Info, "Executing Psexec : \"{0}\" {1}", psexecPath, arguments);

            string commandOutput = string.Empty;
            try
            {
                using (StreamWriter writer = new StreamWriter(tempBatFile))
                {
                    writer.WriteLine(string.Format("@ \"{0}\" {1}",psexecPath, arguments));
                }
                Process proc = Process.Start(tempBatFile);
                proc.WaitForExit();

                using (StreamReader reader = new StreamReader(tempOutputFile))
                {
                    commandOutput = reader.ReadToEnd();
                }
            }
            finally
            {
                try
                {
                    File.Delete(tempBatFile);
                }
                catch { }
                try
                {
                    File.Delete(tempOutputFile);
                }
                catch { }
            }
            return commandOutput;
        }
    }
}
