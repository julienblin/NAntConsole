using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core.Attributes;
using System.DirectoryServices;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS
{
    [TaskName("iis-admin")]
    public class IISAdminTask : BaseIISTask
    {
        private IISAdminAction action;
        [TaskAttribute("action", Required = true)]
        public IISAdminAction Action
        {
            get { return action; }
            set { action = value; }
        }

        protected override void ExecuteTask()
        {
            DirectoryEntry webSite = FindWebSite(WebsiteName);
            if (webSite != null)
                webSite.Invoke(Action.ToString());
        }
    }

    public enum IISAdminAction
    {
        Start,
        Stop,
        Pause,
        Continue
    }
}
