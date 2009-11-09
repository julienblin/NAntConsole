using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Net
{
    [TaskName("gac-install")]
    public class GacInstallTask : Task
    {
        private readonly Publish enterprisePublish = new Publish();

        private FileInfo assembly;
        [TaskAttribute("assembly", Required = true)]
        public FileInfo Assembly
        {
            get { return assembly; }
            set { assembly = value; }
        }

        private FileSet targetFiles;
        [BuildElement("fileset")]
        public FileSet TargetFiles
        {
            get { return targetFiles; }
            set { targetFiles = value; }
        }

        protected override void ExecuteTask()
        {
            if (Assembly != null)
            {
                Log(Level.Info, Resources.GacInstallInstalling, Assembly.FullName);
                enterprisePublish.GacInstall(Assembly.FullName);
            }

            if (TargetFiles != null)
            {
                foreach (string fileName in TargetFiles.FileNames)
                {
                    Log(Level.Info, Resources.GacInstallInstalling, fileName);
                    enterprisePublish.GacInstall(fileName);
                }
            }
        }
    }
}
