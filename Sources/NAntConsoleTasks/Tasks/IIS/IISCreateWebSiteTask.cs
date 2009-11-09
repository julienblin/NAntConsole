using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS
{
    [TaskName("iis-create-website")]
    public class IISCreateWebSiteTask : BaseIISTask
    {
        private DirectoryInfo websitePath;
        [TaskAttribute("path", Required = true)]
        public DirectoryInfo WebsitePath
        {
            get { return websitePath; }
            set { websitePath = value; }
        }

        private IISServerBindings serverBindings;
        [BuildElement("bindings", Required=true)]
        public IISServerBindings ServerBindings
        {
            get { return serverBindings; }
            set { serverBindings = value; }
        }

        private readonly ArrayList siteProperties = new ArrayList();
        [BuildElementArray("property", ElementType = typeof(IISTypedPropertyElement))]
        public ArrayList SiteProperties
        {
            get
            {
                return siteProperties;
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

        private readonly ArrayList webFiles = new ArrayList();
        [BuildElementArray("webfile", ElementType = typeof(IISWebFileElement))]
        public ArrayList WebFiles
        {
            get
            {
                return webFiles;
            }
        }

        protected override void ExecuteTask()
        {
            DirectoryEntry newWebsite = FindWebSite(WebsiteName);
            if (newWebsite == null)
            {
                DirectoryEntry w3svc = new DirectoryEntry(IISConstants.IIS_ADSI_ROOT);
                object[] newSiteParams = new object[] { WebsiteName, GetBindings(), WebsitePath.FullName };
                int siteId = (int)w3svc.Invoke("CreateNewSite", newSiteParams);
                newWebsite = FindWebSite(siteId);
            }

            newWebsite.Invoke("SetInfo");
            ApplyProperties(newWebsite, SiteProperties);
            newWebsite.CommitChanges();

            foreach (IISVDirElement vdir in VDirs)
            {
                CreateVDir(newWebsite, vdir);
            }

            foreach (IISWebFileElement webFile in WebFiles)
            {
                CreateWebFile(newWebsite, webFile);
            }
        }

        private object[] GetBindings()
        {
            List<object> result = new List<object>();
            foreach (MultiStringEntryElement element in ServerBindings.Entries)
            {
                result.Add(element.MultiStringEntryValue);
            }
            return result.ToArray();
        }

        private void CreateVDir(DirectoryEntry parent, IISVDirElement vdir)
        {
            Log(Level.Info, Resources.IISCreateWebSiteCreateVDir, vdir.VDirName);

            DirectoryEntry siteVDir = null;
            foreach(DirectoryEntry childEntry in parent.Children)
            {
                if(childEntry.Name.Equals(vdir.VDirName, StringComparison.InvariantCulture))
                    siteVDir = childEntry;
            }

            if(siteVDir == null)
                siteVDir = parent.Children.Add(vdir.VDirName, IISConstants.TYPE_VDIR);

            siteVDir.Invoke("SetInfo");
            ApplyProperties(siteVDir, vdir.VDirProperties);
            siteVDir.CommitChanges();

            if (vdir.CreateApp)
            {
                siteVDir.Invoke("AppCreate", true);
            }

            foreach (IISVDirElement childrenVDir in vdir.VDirs)
            {
                CreateVDir(siteVDir, childrenVDir);
            }
            siteVDir.CommitChanges();
        }

        private void CreateWebFile(DirectoryEntry parent, IISWebFileElement webFile)
        {

            string targetDirectoryUrl = string.Format("{0}/ROOT/{1}", parent.Path, webFile.Path.Replace("\\", "/"));
            string targetDirectoryPath = string.Format("ROOT/{0}", webFile.Path.Replace("\\", "/"));
            string targetFilePath = string.Format("ROOT/{0}/{1}", webFile.Path.Replace("\\", "/"), webFile.FileName);

            Log(Level.Info, Resources.IISCreateWebSiteCreateWebFile, targetFilePath);

            DirectoryEntry webDirectoryDe;
            if (!DirectoryEntry.Exists(targetDirectoryUrl))
            {
                webDirectoryDe = parent.Children.Add(targetDirectoryPath, "IIsWebDirectory");
                webDirectoryDe.CommitChanges();
            }
            else
            {
                webDirectoryDe = new DirectoryEntry(targetDirectoryUrl);
            }

            DirectoryEntry webFileEntry = webDirectoryDe.Children.Add(webFile.FileName, "IIsWebFile");

            webFileEntry.Invoke("SetInfo");
            ApplyProperties(webFileEntry, webFile.WebFileProperties);
            webFileEntry.CommitChanges();
        }
    }
}
