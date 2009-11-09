using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Functions;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Net
{
    [TaskName("generate-interop")]
    public class GenerateInteropTask : Task
    {
        private const string SN_EXE = @"sn.exe";
        private const string TLBIMP_EXE = @"TlbImp.exe";

        private FileInfo comDll;
        [TaskAttribute("comdll", Required = true)]
        public FileInfo ComDll
        {
            get { return comDll; }
            set { comDll = value; }
        }

        private DirectoryInfo outDir;
        [TaskAttribute("outdir", Required = true)]
        public DirectoryInfo OutDir
        {
            get { return outDir; }
            set { outDir = value; }
        }

        private bool force;
        [TaskAttribute("force")]
        public bool Force
        {
            get { return force; }
            set { force = value; }
        }

        protected override void ExecuteTask()
        {
            if (!ComDll.Exists)
            {
                throw new BuildException(string.Format(Resources.MissingFile, ComDll.FullName));
            }

            string coreName = Path.GetFileNameWithoutExtension(ComDll.FullName);

            FileInfo referenceFile = new FileInfo(Path.Combine(OutDir.FullName, string.Format("Interop.{0}.reference", coreName)));
            FileInfo interopFile = new FileInfo(Path.Combine(OutDir.FullName, string.Format("Interop.{0}.dll", coreName)));
            FileInfo keyFile = new FileInfo(Path.Combine(OutDir.FullName, string.Format("Interop.{0}.snk", coreName)));

            if (!Force)
            {
                if (referenceFile.Exists)
                {
                    if (FileFunctions.AreTheSame(ComDll.FullName, referenceFile.FullName))
                    {
                        if (interopFile.Exists)
                        {
                            Log(Level.Info, string.Format(Resources.GenerateInteropUpToDate, interopFile.FullName, ComDll.FullName));
                            return;
                        }
                    }
                }
            }

            GenerateKeyFile(keyFile);
            GenerateInteropFile(keyFile, interopFile, coreName);
            ComDll.CopyTo(referenceFile.FullName, true);
        }

        private void GenerateInteropFile(FileInfo keyFile, FileInfo interopFile, string coreName)
        {
            if (interopFile.Exists)
            {
                interopFile.Delete();
            }

            string tlbImpExePath = Path.Combine(Project.TargetFramework.SdkDirectory.FullName, TLBIMP_EXE);
            ProcessStartInfo tlbImpProcessStartInfo = new ProcessStartInfo(tlbImpExePath);
            tlbImpProcessStartInfo.UseShellExecute = false;
            tlbImpProcessStartInfo.RedirectStandardOutput = true;
            tlbImpProcessStartInfo.WorkingDirectory = OutDir.FullName;

            tlbImpProcessStartInfo.Arguments = string.Format("\"{0}\" /out:\"{1}\" /namespace:{2} /keyfile:\"{3}\"", ComDll.FullName, interopFile.FullName, coreName, keyFile.FullName);
            tlbImpProcessStartInfo.CreateNoWindow = true;

            int exitCode = Int32.MinValue;
            using (Process tlbImpProc = new Process())
            {
                tlbImpProc.StartInfo = tlbImpProcessStartInfo;

                tlbImpProc.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
                                                 {
                                                     if(!string.IsNullOrEmpty(e.Data))
                                                        Log(Level.Info, e.Data);
                                                 };

                tlbImpProc.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
                                                {
                                                    if (!string.IsNullOrEmpty(e.Data))
                                                        Log(Level.Error, e.Data);
                                                };

                tlbImpProc.Start();
                tlbImpProc.BeginOutputReadLine();
                tlbImpProc.WaitForExit();
                exitCode = tlbImpProc.ExitCode;
            }

            if (exitCode != 0)
            {
                throw new BuildException(Resources.GenerateInteropErrorExecutingTlbimp);
            }
        }

        private void GenerateKeyFile(FileInfo keyFile)
        {
            if(!keyFile.Exists)
            {
                string snExePath = Path.Combine(Project.TargetFramework.SdkDirectory.FullName, SN_EXE);
                ProcessStartInfo snProcessStartInfo = new ProcessStartInfo(snExePath);
                snProcessStartInfo.UseShellExecute = false;
                snProcessStartInfo.RedirectStandardOutput = true;
                snProcessStartInfo.WorkingDirectory = OutDir.FullName;

                snProcessStartInfo.Arguments = string.Format("-k \"{0}\"", keyFile.FullName);
                snProcessStartInfo.CreateNoWindow = true;

                int exitCode = Int32.MinValue;
                using (Process snProc = new Process())
                {
                    snProc.StartInfo = snProcessStartInfo;

                    snProc.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
                                                     {
                                                         if (!string.IsNullOrEmpty(e.Data))
                                                             Log(Level.Info, e.Data);
                                                     };

                    snProc.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
                                                    {
                                                        if (!string.IsNullOrEmpty(e.Data))
                                                            Log(Level.Error, e.Data);
                                                    };

                    snProc.Start();
                    snProc.BeginOutputReadLine();
                    snProc.WaitForExit();
                    exitCode = snProc.ExitCode;
                }

                if (exitCode != 0)
                {
                    throw new BuildException(Resources.GenerateInteropErrorExecutingSn);
                }
            }
        }
    }
}
