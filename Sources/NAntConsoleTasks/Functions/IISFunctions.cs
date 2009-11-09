using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core.Attributes;
using System.DirectoryServices;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS;
using NAnt.Core;
using System.Diagnostics;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Functions
{
    [FunctionSet("iis", "iis")]
    public class IISFunctions : FunctionSetBase
    {
        public IISFunctions(Project project, PropertyDictionary properties)
            : base(project, properties)
        {
        }

        [Function("get-apppool-property")]
        public static string GetApppoolProperty(string apppoolName, string propertyName)
        {
            DirectoryEntry appPool = BaseAppPoolTask.FindAppPool(apppoolName);
            if (appPool != null)
            {
                return appPool.Properties[propertyName] != null ? appPool.Properties[propertyName].Value.ToString() : string.Empty;
            }
            return string.Empty;
        }

        [Function("get-website-property")]
        public static string GetWebsiteProperty(string websiteName, string propertyName, string vdir)
        {
            DirectoryEntry entry = BaseIISTask.FindWebSite(websiteName);
            if (!string.IsNullOrEmpty(vdir))
            {
                entry = new DirectoryEntry(entry.Path + vdir);
            }

            if (entry != null)
            {
                return entry.Properties[propertyName] != null ? entry.Properties[propertyName].Value.ToString() : string.Empty;
            }
            return string.Empty;
        }
    }
}
