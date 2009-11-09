using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core.Attributes;
using System.DirectoryServices;
using NAnt.Core;
using System.Collections;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS
{

    public abstract class BaseAppPoolTask : Task
    {
        private string apppoolName;
        [TaskAttribute("name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string AppPoolName
        {
            get { return apppoolName; }
            set { apppoolName = value; }
        }

        internal static DirectoryEntry FindAppPool(string name)
        {
            DirectoryEntry appPoolRoot =  new DirectoryEntry(IISConstants.IIS_ADSI_APPPOOL_ROOT);
            foreach (DirectoryEntry childEntry in appPoolRoot.Children)
            {
                if (childEntry.Name.Equals(name))
                    return childEntry;
            }
            return null;
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
