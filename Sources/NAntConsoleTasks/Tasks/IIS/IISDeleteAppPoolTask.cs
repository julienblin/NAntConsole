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
    [TaskName("iis-delete-apppool")]
    public class IISDeleteAppPoolTask : BaseAppPoolTask
    {
        protected override void ExecuteTask()
        {
            DirectoryEntry foundEntry = FindAppPool(AppPoolName);
            if (foundEntry != null)
            {
                DirectoryEntry appPoolsRoot = new DirectoryEntry(IISConstants.IIS_ADSI_APPPOOL_ROOT);
                appPoolsRoot.Children.Remove(foundEntry);
                appPoolsRoot.CommitChanges();
            }
        }
    }
}
