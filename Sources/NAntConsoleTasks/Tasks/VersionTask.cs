using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks
{
    [TaskName("version")]
    public class VersionTask : Task
    {
        private const int BEGINNING_OF_THE_CENTURY = 2000;

        private string propertyName = EnvIncludeConstants.DEFAULT_VERSION_PROPERTY_NAME;
        [TaskAttribute("property")]
        [StringValidator(AllowEmpty = false)]
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        protected override void ExecuteTask()
        {
            Version version = null;
            if (Properties.Contains(PropertyName))
            {
                return;
            }
            FileInfo versionFile = new FileInfo(Project.GetFullPath(EnvIncludeConstants.VERSION_FILENAME));
            if (versionFile.Exists)
            {
                using (StreamReader reader = new StreamReader(versionFile.FullName))
                {
                    version = new Version(reader.ReadLine());
                }
            }
            else
            {
                version = new Version(DateTime.Today.Year - BEGINNING_OF_THE_CENTURY, DateTime.Today.Month, DateTime.Today.Day, CalculateSecondsSinceMidnight());
            }

            Log(Level.Info, Resources.VersionSet, PropertyName, version);
            Project.Properties.Add(PropertyName, version.ToString());
        }

        private static int CalculateSecondsSinceMidnight()
        {
            return (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second) / 10;
        }
    }
}
