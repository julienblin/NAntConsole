using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS
{
    public static class IISConstants
    {
        public const string IIS_ADSI_ROOT = @"IIS://localhost/W3SVC";
        public const string IIS_ADSI_APPPOOL_ROOT = IIS_ADSI_ROOT + @"/AppPools";
        public const string PROPERTY_WEBSITE_NAME = @"ServerComment";

        public const string TYPE_WEBSERVICE = @"IIsWebService";
        public const string TYPE_APPPOOL = @"IIsApplicationPool";
        public const string TYPE_WEBSERVER = @"IIsWebServer";
        public const string TYPE_VDIR = @"IIsWebVirtualDir";

        public const string APPPOOL_SUFFIX = @"AppPool";



        public static DirectoryEntry GetAppPool(string appPoolId)
        {
            DirectoryEntry appPoolsRoot = new DirectoryEntry(IIS_ADSI_APPPOOL_ROOT);
            foreach (DirectoryEntry directoryEntry in appPoolsRoot.Children)
            {
                if (directoryEntry.Path.Equals(string.Concat(IIS_ADSI_APPPOOL_ROOT, "/", appPoolId)))
                {
                    return directoryEntry;
                }
            }
            return null;
        }

        public static T GetProperty<T>(DirectoryEntry directoryEntry, string propertyName)
        {
            PropertyValueCollection propertyValueCollection = directoryEntry.Properties[propertyName];
            return propertyValueCollection != null ? ((T)propertyValueCollection.Value) : default(T);
        }
    }
}
