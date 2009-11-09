using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using System.DirectoryServices;
using System.Diagnostics;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types;
using System.Collections;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS
{
    [TaskName("iis-create-apppool")]
    public class IISCreateAppPoolTask : BaseAppPoolTask
    {
        private readonly ArrayList appPoolProperties = new ArrayList();
        [BuildElementArray("property", ElementType = typeof(IISTypedPropertyElement))]
        public ArrayList AppPoolProperties
        {
            get
            {
                return appPoolProperties;
            }
        }

        protected override void ExecuteTask()
        {
            DirectoryEntry foundEntry = FindAppPool(AppPoolName);
            if (foundEntry != null)
            {
                Log(Level.Info, Resources.IISAppPoolAlreadyExists, AppPoolName);
            }
            else
            {
                Log(Level.Info, Resources.IISCreateWebSiteCreateAppPool, AppPoolName);
                DirectoryEntry appPoolsRoot = new DirectoryEntry(IISConstants.IIS_ADSI_APPPOOL_ROOT);
                foundEntry = (DirectoryEntry)appPoolsRoot.Invoke("Create", IISConstants.TYPE_APPPOOL, AppPoolName);
            }

            foundEntry.Invoke("SetInfo");
            ApplyProperties(foundEntry, AppPoolProperties);
            foundEntry.CommitChanges();
        }
    }
}
