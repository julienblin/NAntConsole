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
    [TaskName("gac-uninstall")]
    public class GacUninstallTask : Task
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
                if (Assembly.Exists)
                {
                    Log(Level.Info, Resources.GacUninstallRemove, Assembly.FullName);
                    enterprisePublish.GacRemove(Assembly.FullName);
                }
                else
                {
                    Log(Level.Warning, Resources.MissingFile, Assembly.FullName);
                }
            }

            if (TargetFiles != null)
            {
                foreach (string fileName in TargetFiles.FileNames)
                {
                    FileInfo file = new FileInfo(fileName);

                    if (file.Exists)
                    {
                        Log(Level.Info, Resources.GacUninstallRemove, file.FullName);
                        enterprisePublish.GacRemove(file.FullName);
                    }
                    else
                    {
                        Log(Level.Warning, Resources.MissingFile, file.FullName);
                    }
                }
            }
        }
    }
}
