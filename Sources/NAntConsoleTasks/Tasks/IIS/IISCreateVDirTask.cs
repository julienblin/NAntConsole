using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS
{
    [TaskName("iis-create-vdir")]
    public class IISCreateVDirTask : BaseIISTask
    {
        private string vdirName;
        [TaskAttribute("name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string VDirName
        {
            get { return vdirName; }
            set { vdirName = value; }
        }

        private readonly ArrayList vdirProperties = new ArrayList();
        [BuildElementArray("property", ElementType = typeof(IISTypedPropertyElement))]
        public ArrayList VDirProperties
        {
            get
            {
                return vdirProperties;
            }
        }

        private readonly ArrayList vdirs = new ArrayList();
        [BuildElementArray("vdir", ElementType = typeof(IISVDirElement))]
        public ArrayList VDirs
        {
            get
            {
                return vdirs;
            }
        }

        protected override void ExecuteTask()
        {
            IISDeleteVDirTask deleteVDirTask = new IISDeleteVDirTask();
            CopyTo(deleteVDirTask);
            deleteVDirTask.Parent = this;
            deleteVDirTask.WebsiteName = WebsiteName;
            deleteVDirTask.VDirName = VDirName;
            deleteVDirTask.Execute();

            DirectoryEntry webSiteDirectory = FindWebSite(WebsiteName);
            if (webSiteDirectory != null)
            {
                if (DirectoryEntry.Exists(webSiteDirectory.Path + "/Root"))
                {
                    DirectoryEntry rootWebSite = GetWebSiteRoot(webSiteDirectory);
                    Log(Level.Info, Resources.IISCreateVDirCreate, WebsiteName, VDirName);
                    DirectoryEntry siteVDir = rootWebSite.Children.Add(VDirName, IISConstants.TYPE_VDIR);
                    ApplyProperties(siteVDir, VDirProperties);
                    siteVDir.CommitChanges();
                    foreach (IISVDirElement childrenVDir in VDirs)
                    {
                        CreateVDir(siteVDir, childrenVDir);
                    }
                    siteVDir.CommitChanges();
                }
                else
                {
                    throw new BuildException(string.Format(Resources.IISWebSiteRootNotFound, WebsiteName));
                }
            }
            else
            {
                throw new BuildException(string.Format(Resources.IISWebSiteNotFound, WebsiteName));
            }
        }

        private static void CreateVDir(DirectoryEntry parent, IISVDirElement vdir)
        {
            DirectoryEntry siteVDir = parent.Children.Add(vdir.VDirName, IISConstants.TYPE_VDIR);
            ApplyProperties(siteVDir, vdir.VDirProperties);
            siteVDir.CommitChanges();
            foreach (IISVDirElement childrenVDir in vdir.VDirs)
            {
                CreateVDir(siteVDir, childrenVDir);
            }
            siteVDir.CommitChanges();
        }
    }
}
