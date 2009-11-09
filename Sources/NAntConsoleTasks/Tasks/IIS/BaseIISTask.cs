using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS
{
    public abstract class BaseIISTask : Task
    {
        private string websiteName;
        [TaskAttribute("website", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string WebsiteName
        {
            get { return websiteName; }
            set { websiteName = value; }
        }

        internal static DirectoryEntry FindWebSite(string webSiteName)
        {
            DirectoryEntry rootDirectoryEntry = new DirectoryEntry(IISConstants.IIS_ADSI_ROOT);
            foreach (DirectoryEntry childDirectoryEntry in rootDirectoryEntry.Children)
            {
                if (childDirectoryEntry.SchemaClassName.Equals(IISConstants.TYPE_WEBSERVER,
                                                               StringComparison.InvariantCultureIgnoreCase))
                {
                    string serverComment = IISConstants.GetProperty<string>(childDirectoryEntry, IISConstants.PROPERTY_WEBSITE_NAME);
                    if (!string.IsNullOrEmpty(serverComment) &&
                        serverComment.Equals(webSiteName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return childDirectoryEntry;
                    }
                }
            }
            return null;
        }

        internal static DirectoryEntry FindWebSite(int webSiteId)
        {
            return new DirectoryEntry(IISConstants.IIS_ADSI_ROOT + "/" + webSiteId.ToString());
        }

        protected static DirectoryEntry GetWebSiteRoot(DirectoryEntry webSite)
        {
            return new DirectoryEntry(webSite.Path + "/Root");
        }

        protected static void ApplyProperties(DirectoryEntry directoryEntry, ArrayList properties)
        {
            foreach (IISTypedPropertyElement typedPropertyElement in properties)
            {
                switch (typedPropertyElement.PropertyType)
                {
                    case IISPropertyType.String:
                        directoryEntry.Properties[typedPropertyElement.PropertyName][0] = typedPropertyElement.PropertyValue;
                        break;
                    case IISPropertyType.DWORD:
                        directoryEntry.Properties[typedPropertyElement.PropertyName][0] = Convert.ToInt64(typedPropertyElement.PropertyValue);
                        break;
                    case IISPropertyType.MultiString:
                        if (typedPropertyElement.Replace)
                        {
                            directoryEntry.Properties[typedPropertyElement.PropertyName].Clear();
                        }
         
                        foreach (MultiStringEntryElement entryElement in typedPropertyElement.MultiStringEntries)
                        {
                            directoryEntry.Properties[typedPropertyElement.PropertyName].Add(entryElement.MultiStringEntryValue);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
