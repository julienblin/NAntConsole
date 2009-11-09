using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.COM
{
    [TaskName("com-delete-application")]
    public class COMDeleteApplicationTask : BaseCOMTask
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
            using (COMSearchResult searchResult = FindApplication(ApplicationName))
            {
                if (searchResult.NotFound)
                {
                    Log(Level.Info, Resources.COMDeleteApplicationNotFound, ApplicationName);
                }
                else
                {
                    COMStopApplicationTask comStopApplicationTask = new COMStopApplicationTask();
                    CopyTo(comStopApplicationTask);
                    comStopApplicationTask.Parent = this;
                    comStopApplicationTask.ApplicationName = ApplicationName;
                    comStopApplicationTask.Execute();

                    Log(Level.Info, Resources.COMDeleteApplicationDeleting, ApplicationName);
                    searchResult.CatalogCollection.Remove(searchResult.Index);
                    searchResult.CatalogCollection.SaveChanges();
                }

                searchResult.CatalogCollection.SaveChanges();
            }
        }
    }
}
