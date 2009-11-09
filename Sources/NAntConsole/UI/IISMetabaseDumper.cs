using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public class IISMetabaseDumper
    {
        List<string> filteredProperties = new List<string>();
        List<string> multiStringProperties = new List<string>();

        public IISMetabaseDumper()
        {
            filteredProperties.Add(IISConstants.PROPERTY_WEBSITE_NAME);
            //filteredProperties.Add(IISConstants.PROPERTY_APPPOOLID_NAME);
            filteredProperties.Add("KeyType");
            filteredProperties.Add("UNCPassword");
            filteredProperties.Add("AppRoot");
            
            multiStringProperties.Add("ServerBindings");
        }

        public string DumpWebsite(DirectoryEntry entry)
        {
            StringBuilder result = new StringBuilder();
            string webSiteName = IISConstants.GetProperty<string>(entry, IISConstants.PROPERTY_WEBSITE_NAME);
            result.AppendFormat("<iis-create-website website=\"{0}\">", webSiteName);
            result.AppendLine();
            DumpProperties(result, entry, 1);
            DumpAppPool(result, entry, 1);
            foreach (DirectoryEntry child in entry.Children)
            {
                if (child.SchemaClassName.Equals(IISConstants.TYPE_VDIR, StringComparison.InvariantCulture))
                {
                    DumpInsideVDir(result, child, 1);
                }
            }
            result.AppendLine("</iis-create-website>");
            return result.ToString();
        }

        public string DumpVDir(DirectoryEntry entry)
        {
            StringBuilder result = new StringBuilder();

            DirectoryEntry webSiteEntry = entry.Parent.Parent;
            if (webSiteEntry.SchemaClassName.Equals(IISConstants.TYPE_WEBSERVER, StringComparison.InvariantCulture))
            {
                string webSiteName = IISConstants.GetProperty<string>(webSiteEntry, IISConstants.PROPERTY_WEBSITE_NAME);
                result.AppendFormat("<iis-create-vdir name=\"{0}\" website=\"{1}\">", entry.Name, webSiteName);
                result.AppendLine();
                DumpProperties(result, entry, 1);
                foreach (DirectoryEntry child in entry.Children)
                {
                    if (child.SchemaClassName.Equals(IISConstants.TYPE_VDIR, StringComparison.InvariantCulture))
                    {
                        DumpInsideVDir(result, child, 1);
                    }
                }
                result.AppendLine("</iis-create-vdir>");
            } 
            return result.ToString();
        }

        private void DumpInsideVDir(StringBuilder result, DirectoryEntry entry, int indent)
        {
            string indentString = new string('\t', indent);
            result.AppendFormat("{0}<vdir name=\"{1}\">", indentString, entry.Name);
            result.AppendLine();
            DumpProperties(result, entry, indent + 1);
            foreach (DirectoryEntry child in entry.Children)
            {
                if (child.SchemaClassName.Equals(IISConstants.TYPE_VDIR, StringComparison.InvariantCulture))
                {
                    DumpInsideVDir(result, child, indent + 1);
                }
            }
            result.AppendFormat("{0}</vdir>", indentString);
            result.AppendLine();
        }

        private void DumpAppPool(StringBuilder result, DirectoryEntry entry, int indent)
        {
            string indentString = new string('\t', indent);
            //string appPoolId = IISConstants.GetProperty<string>(entry, IISConstants.PROPERTY_APPPOOLID_NAME);
            //DirectoryEntry appPoolDE = IISConstants.GetAppPool(appPoolId);
            //result.AppendFormat("{0}<apppool>", indentString);
            //result.AppendLine();
            //DumpProperties(result, appPoolDE, indent + 1);
            //result.AppendFormat("{0}</apppool>", indentString);
            //result.AppendLine();
        }

        private void DumpProperties(StringBuilder result, DirectoryEntry entry, int indent)
        {
            string indentString = new string('\t', indent);
            foreach (PropertyValueCollection propertyValueCollection in entry.Properties)
            {
                if (!filteredProperties.Contains(propertyValueCollection.PropertyName))
                {
                    if (!ArePropertyValuesEquals(propertyValueCollection.Value, FoundParentPropertyValue(entry, propertyValueCollection.PropertyName)))
                    {
                        string propertyType = "String";
                        object propertyValue = propertyValueCollection.Value;
                        bool written = false;

                        if (propertyValueCollection.Value is bool)
                        {
                            propertyType = IISPropertyType.DWORD.ToString();
                            propertyValue = ((bool)propertyValue) ? 1 : 0;
                        }

                        if (propertyValueCollection.Value is int)
                        {
                            propertyType = IISPropertyType.DWORD.ToString();
                        }

                        if (propertyValueCollection.Value is System.Object[])
                        {
                            result.AppendFormat("{0}<property name=\"{1}\" type=\"{2}\">", indentString,
                                                propertyValueCollection.PropertyName,
                                                IISPropertyType.MultiString.ToString());
                            result.AppendLine();
                            foreach (object multiStringValue in (System.Object[])propertyValueCollection.Value)
                            {
                                result.AppendFormat("{0}\t<entry value=\"{1}\" />", indentString, multiStringValue);
                                result.AppendLine();
                            }
                            result.AppendFormat("{0}</property>", indentString);
                            result.AppendLine();
                            written = true;
                        }

                        if (propertyValue.GetType().Name.Equals("__ComObject", StringComparison.InvariantCulture))
                        {
                            written = true;
                        }

                        // Strange special case...
                        if (multiStringProperties.Contains(propertyValueCollection.PropertyName) && !written)
                        {
                            result.AppendFormat("{0}<property name=\"{1}\" type=\"{2}\">", indentString,
                                                propertyValueCollection.PropertyName,
                                                IISPropertyType.MultiString.ToString());
                            result.AppendLine();
                            result.AppendFormat("{0}\t<entry value=\"{1}\" />", indentString, propertyValue);
                            result.AppendLine();
                            result.AppendFormat("{0}</property>", indentString);
                            result.AppendLine();
                            written = true;
                        }

                        if (!written)
                        {
                            result.AppendFormat("{0}<property name=\"{1}\" value=\"{2}\" type=\"{3}\" />", indentString,
                                                propertyValueCollection.PropertyName, propertyValue, propertyType);
                            result.AppendLine();
                        }
                    }
                }
            }
        }

        private static bool ArePropertyValuesEquals(object value, object value2)
        {
            if (value is System.Object[])
            {
                object[] tabValue = (object[])value;
                object[] tabValue2 = (object[])value2;
                if (tabValue.Length != tabValue2.Length)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < tabValue.Length; ++i)
                    {
                        if(!tabValue[i].Equals(tabValue2[i]))
                            return false;
                    }
                    return true;
                }
            }
            else
            {
                return value.Equals(value2);
            }
        }

        private static object FoundParentPropertyValue(DirectoryEntry entry, string propertyName)
        {
            KeyValuePair<DirectoryEntry, object> resultRecur = FoundParentPropertyValueRecur(entry, propertyName);
            if(resultRecur.Key == null)
                return null;

            if(resultRecur.Key == entry)
                return null;

            return resultRecur.Value;
        }

        private static KeyValuePair<DirectoryEntry, object> FoundParentPropertyValueRecur(DirectoryEntry entry, string propertyName)
        {
            if (!entry.Parent.Path.Equals(@"IIS:"))
            {
                if (entry.Parent.Properties.Contains(propertyName))
                {
                    return FoundParentPropertyValueRecur(entry.Parent, propertyName);
                }
                else
                {
                    return new KeyValuePair<DirectoryEntry, object>(entry, entry.Properties[propertyName].Value);
                }
            }
            else
            {
                return new KeyValuePair<DirectoryEntry, object>(null, null);
            }
        }
    }
}
