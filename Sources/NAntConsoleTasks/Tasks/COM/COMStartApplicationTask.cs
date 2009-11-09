using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.COM
{
    [TaskName("com-start-application")]
    public class COMStartApplicationTask : BaseCOMTask
    {
        private string applicationName;
        [TaskAttribute("name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        protected override void ExecuteCOMTask()
        {
            using (COMSearchResult appSearchResult = FindApplication(ApplicationName))
            {
                if (appSearchResult.NotFound)
                {
                    Log(Level.Info, Resources.COMStartApplicationNotFound, ApplicationName);
                }
                else
                {
                    Log(Level.Info, Resources.COMStartApplicationStarting, ApplicationName);
                    ComAdminCatalog.StartApplication(ApplicationName);
                }
            }
        }
    }
}
