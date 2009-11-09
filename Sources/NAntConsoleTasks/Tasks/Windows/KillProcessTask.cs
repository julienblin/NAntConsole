using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows
{
    [TaskName("kill-process")]
    public class KillProcessTask : Task
    {
        private FileInfo dll;
        [TaskAttribute("dll")]
        public FileInfo Dll
        {
            get { return dll; }
            set { dll = value; }
        }

        private string processName;
        [TaskAttribute("process-name")]
        public string ProcessName
        {
            get { return processName; }
            set { processName = value; }
        }

        protected override void ExecuteTask()
        {
            if (!string.IsNullOrEmpty(processName))
            {
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    Log(Level.Info, string.Format(Resources.KillProcessKill, process.ProcessName));
                    Kill(process);
                }
            }

            if (Dll != null)
                if(Dll.Exists)
                {
                    Log(Level.Info, string.Format(Resources.KillProcessSearching, Dll.FullName));
                    Process[] processes = Process.GetProcesses();
                    foreach (Process process in processes)
                    {
                        try
                        {
                            foreach (ProcessModule module in process.Modules)
                            {
                                if (module.FileName.Equals(Dll.FullName, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Log(Level.Info, string.Format(Resources.KillProcessFoundProcess, process.ProcessName, Dll.FullName));
                                    Kill(process);
                                    break;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
        }

        private void Kill(Process process)
        {
            try
            {
                process.Kill();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                Log(Level.Error, string.Format(Resources.KillProcessError, process.ProcessName, ex));
            }
        }
    }
}
