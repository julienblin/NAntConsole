using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS
{
    [TaskName("iis-delete-website")]
    public class IISDeleteWebSiteTask : BaseIISTask
    {
        protected override void ExecuteTask()
        {
            DirectoryEntry webSiteDirectory = FindWebSite(WebsiteName);
            if (webSiteDirectory != null)
            {
                Log(Level.Info, Resources.IISDeleteWebSiteDeleting, WebsiteName);
                DirectoryEntry root = webSiteDirectory.Parent;

                root.Children.Remove(webSiteDirectory);
                root.CommitChanges();
            }
            else
            {
                Log(Level.Warning, Resources.IISWebSiteNotFound, WebsiteName);
            }
        }
    }
}
