using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS
{
    [TaskName("iis-delete-vdir")]
    public class IISDeleteVDirTask : BaseIISTask
    {
        private string vdirName;
        [TaskAttribute("name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string VDirName
        {
            get { return vdirName; }
            set { vdirName = value; }
        }

        protected override void ExecuteTask()
        {
            DirectoryEntry webSiteDirectory = FindWebSite(WebsiteName);
            if (webSiteDirectory != null)
            {
                if (DirectoryEntry.Exists(webSiteDirectory.Path + "/Root"))
                {
                    DirectoryEntry rootWebSite = GetWebSiteRoot(webSiteDirectory);
                    if (DirectoryEntry.Exists(rootWebSite.Path + "/" + VDirName))
                    {
                        Log(Level.Info, Resources.IISDeleteVDirDelete, WebsiteName, VDirName);
                        DirectoryEntry targetVdir = new DirectoryEntry(rootWebSite.Path + "/" + VDirName);
                        DirectoryEntry parentVdir = targetVdir.Parent;
                        parentVdir.Children.Remove(targetVdir);
                        parentVdir.CommitChanges();
                    }
                }
            }
        }
    }
}
