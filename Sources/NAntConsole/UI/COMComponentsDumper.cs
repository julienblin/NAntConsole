using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.COM;
using Comadmin;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public class COMComponentsDumper
    {
        List<ComponentProperties> appProperties = new List<ComponentProperties>();
        List<ComponentProperties> componentsProperties = new List<ComponentProperties>();

        public COMComponentsDumper()
        {
            appProperties.Add(new ComponentProperties("3GigSupportEnabled", typeof(bool), false));
            appProperties.Add(new ComponentProperties("AccessChecksLevel", typeof(COMAdminAccessChecksLevelOptions), COMAdminAccessChecksLevelOptions.COMAdminAccessChecksApplicationComponentLevel));
            appProperties.Add(new ComponentProperties("Activation", typeof(COMAdminActivationOptions), COMAdminActivationOptions.COMAdminActivationLocal));
            appProperties.Add(new ComponentProperties("ApplicationAccessChecksEnabled", typeof(bool), true));
            appProperties.Add(new ComponentProperties("ApplicationDirectory", typeof(string), string.Empty));
            appProperties.Add(new ComponentProperties("ApplicationProxyServerName", typeof(string), string.Empty));
            appProperties.Add(new ComponentProperties("Authentication", typeof(COMAdminAuthenticationLevelOptions), COMAdminAuthenticationLevelOptions.COMAdminAuthenticationPacket));
            appProperties.Add(new ComponentProperties("AuthenticationCapability", typeof(COMAdminAuthenticationCapabilitiesOptions), COMAdminAuthenticationCapabilitiesOptions.COMAdminAuthenticationCapabilitiesDynamicCloaking));
            appProperties.Add(new ComponentProperties("Changeable", typeof(bool), true));
            appProperties.Add(new ComponentProperties("CommandLine", typeof(string), string.Empty));
            appProperties.Add(new ComponentProperties("ConcurrentApps", typeof(int), 1));
            appProperties.Add(new ComponentProperties("CreatedBy", typeof(string), string.Empty));
            appProperties.Add(new ComponentProperties("CRMEnabled", typeof(bool), false));
            appProperties.Add(new ComponentProperties("CRMLogFile", typeof(string), string.Empty));
            appProperties.Add(new ComponentProperties("Deleteable", typeof(bool), true));
            appProperties.Add(new ComponentProperties("Description", typeof(string), string.Empty));
            appProperties.Add(new ComponentProperties("DumpEnabled", typeof(bool), false));
            appProperties.Add(new ComponentProperties("DumpOnException", typeof(bool), false));
            appProperties.Add(new ComponentProperties("DumpOnFailfast", typeof(bool), false));
            appProperties.Add(new ComponentProperties("DumpPath", typeof(string), @"%systemroot%\system32\com\dmp"));
            appProperties.Add(new ComponentProperties("EventsEnabled", typeof(bool), true));
            appProperties.Add(new ComponentProperties("Identity", typeof(string), @"Interactive User"));
            appProperties.Add(new ComponentProperties("ImpersonationLevel", typeof(COMAdminImpersonationLevelOptions), COMAdminImpersonationLevelOptions.COMAdminImpersonationImpersonate));
            appProperties.Add(new ComponentProperties("IsEnabled", typeof(bool), true));
            appProperties.Add(new ComponentProperties("IsSystem", typeof(bool), false));
            appProperties.Add(new ComponentProperties("MaxDumpCount", typeof(int), 5));
            appProperties.Add(new ComponentProperties("Password", typeof(string), string.Empty));
            appProperties.Add(new ComponentProperties("QueueListenerEnabled", typeof(bool), false));
            appProperties.Add(new ComponentProperties("QueuingEnabled", typeof(bool), false));
            appProperties.Add(new ComponentProperties("RecycleActivationLimit", typeof(int), 0));
            appProperties.Add(new ComponentProperties("RecycleCallLimit", typeof(int), 0));
            appProperties.Add(new ComponentProperties("RecycleExpirationTimeout", typeof(int), 15));
            appProperties.Add(new ComponentProperties("RecycleLifetimeLimit", typeof(int), 0));
            appProperties.Add(new ComponentProperties("RecycleMemoryLimit", typeof(int), 0));
            appProperties.Add(new ComponentProperties("Replicable", typeof(bool), true));
            appProperties.Add(new ComponentProperties("RunForever", typeof(bool), false));
            appProperties.Add(new ComponentProperties("ShutdownAfter", typeof(int), 3));
            appProperties.Add(new ComponentProperties("SoapActivated", typeof(bool), false));
            appProperties.Add(new ComponentProperties("SoapBaseUrl", typeof(string), string.Empty));
            appProperties.Add(new ComponentProperties("SoapMailTo", typeof(string), string.Empty));
            appProperties.Add(new ComponentProperties("SoapVRoot", typeof(string), string.Empty));
            appProperties.Add(new ComponentProperties("SRPEnabled", typeof(bool), false));

            componentsProperties.Add(new ComponentProperties("AllowInprocSubscribers", typeof(bool), true));
            componentsProperties.Add(new ComponentProperties("ComponentAccessChecksEnabled", typeof(bool), false));
            componentsProperties.Add(new ComponentProperties("ComponentTransactionTimeout", typeof(int), 0));
            componentsProperties.Add(new ComponentProperties("ComponentTransactionTimeoutEnabled", typeof(bool), false));
            componentsProperties.Add(new ComponentProperties("COMTIIntrinsics", typeof(bool), false));
            componentsProperties.Add(new ComponentProperties("ConstructorString", typeof(string), string.Empty));
            componentsProperties.Add(new ComponentProperties("CreationTimeout", typeof(int), 60000));
            componentsProperties.Add(new ComponentProperties("EventTrackingEnabled", typeof(bool), true));
            componentsProperties.Add(new ComponentProperties("ExceptionClass", typeof(string), string.Empty));
            componentsProperties.Add(new ComponentProperties("FireInParallel", typeof(bool), false));
            componentsProperties.Add(new ComponentProperties("IISIntrinsics", typeof(bool), true));
            componentsProperties.Add(new ComponentProperties("InitializesServerApplication", typeof(bool), false));
            componentsProperties.Add(new ComponentProperties("IsEnabled", typeof(bool), true));
            componentsProperties.Add(new ComponentProperties("IsPrivateComponent", typeof(bool), false));
            componentsProperties.Add(new ComponentProperties("JustInTimeActivation", typeof(bool), true));
            componentsProperties.Add(new ComponentProperties("LoadBalancingSupported", typeof(bool), false));
            componentsProperties.Add(new ComponentProperties("MaxPoolSize", typeof(int), 1048576));
            componentsProperties.Add(new ComponentProperties("MinPoolSize", typeof(int), 0));
            componentsProperties.Add(new ComponentProperties("MultiInterfacePublisherFilterCLSID", typeof(string), string.Empty));
            componentsProperties.Add(new ComponentProperties("MustRunInClientContext", typeof(bool), false));
            componentsProperties.Add(new ComponentProperties("MustRunInDefaultContext", typeof(bool), false));
            componentsProperties.Add(new ComponentProperties("ObjectPoolingEnabled", typeof(bool), false));
            componentsProperties.Add(new ComponentProperties("PublisherID", typeof(string), string.Empty));
            componentsProperties.Add(new ComponentProperties("SoapAssemblyName", typeof(string), string.Empty));
            componentsProperties.Add(new ComponentProperties("SoapTypeName", typeof(string), string.Empty));
            componentsProperties.Add(new ComponentProperties("Synchronization", typeof(COMAdminSynchronizationOptions), COMAdminSynchronizationOptions.COMAdminSynchronizationRequired));
            componentsProperties.Add(new ComponentProperties("ThreadingModel", typeof(COMAdminThreadingModels), COMAdminThreadingModels.COMAdminThreadingModelApartment));
        }

        public string DumpApplication(ICOMAdminCatalog catalog, COMAdminCatalogObject application)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("<com-create-application name=\"{0}\">", application.Name);
            result.AppendLine();
            ExtractApplicationProperties(result, catalog, application);
            ExtractApplicationRoles(result, catalog, application);
            ExtractInstallComponent(result, catalog, application);
            ExtractComponentProperties(result, catalog, application);
            result.AppendLine("</com-create-application>");
            return result.ToString();
        }

        private void ExtractApplicationProperties(StringBuilder result, ICOMAdminCatalog catalog, ICatalogObject application)
        {
            ICatalogCollection appCatalog = (ICatalogCollection)catalog.GetCollection(COMConstants.APPLICATIONS_CATALOG_NAME);
            appCatalog.Populate();

            foreach (COMAdminCatalogObject app in appCatalog)
            {
                if (app.Key.Equals(application.Key))
                {
                    foreach (ComponentProperties property in appProperties)
                    {
                        try
                        {
                            object propertyValue = app.get_Value(property.Name);
                            if (propertyValue.GetType() != property.Type)
                            {
                                TypeConverter converter = TypeDescriptor.GetConverter(property.Type);
                                if (converter is EnumConverter)
                                {
                                    foreach (int value in Enum.GetValues(property.Type))
                                    {
                                        if (value == (int)propertyValue)
                                        {
                                            propertyValue = Enum.ToObject(property.Type, (int) propertyValue);
                                        }
                                    }
                                }
                                else
                                {
                                    propertyValue = converter.ConvertFrom(propertyValue);
                                }
                            }
                            if (!propertyValue.Equals(property.DefaultValue))
                            {
                                result.AppendFormat("\t<property name=\"{0}\" value=\"{1}\" />", property.Name, propertyValue);
                                result.AppendLine();
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        private static void ExtractApplicationRoles(StringBuilder result, ICOMAdminCatalog catalog, COMAdminCatalogObject application)
        {
            ICatalogCollection appCatalog = (ICatalogCollection)catalog.GetCollection(COMConstants.APPLICATIONS_CATALOG_NAME);
            appCatalog.Populate();

            foreach (COMAdminCatalogObject app in appCatalog)
            {
                if (app.Key.Equals(application.Key))
                {
                    ICatalogCollection rolesCatalog = (ICatalogCollection)appCatalog.GetCollection(COMConstants.ROLES_CATALOG_NAME, app.Key);
                    rolesCatalog.Populate();
                    foreach (COMAdminCatalogObject role in rolesCatalog)
                    {
                        result.AppendFormat("\t<role name=\"{0}\">", role.get_Value(@"Name"));
                        result.AppendLine();

                        ICatalogCollection usersInRolesCatalog = (ICatalogCollection)rolesCatalog.GetCollection(COMConstants.USERS_IN_ROLES_CATALOG_NAME, role.Key);
                        usersInRolesCatalog.Populate();
                        foreach (COMAdminCatalogObject user in usersInRolesCatalog)
                        {
                            result.AppendFormat("\t\t<user name=\"{0}\" />", user.get_Value(@"User"));
                            result.AppendLine();
                        }

                        result.Append("\t</role>");
                        result.AppendLine();
                    }
                }
            }
        }

        private static void ExtractInstallComponent(StringBuilder result, ICOMAdminCatalog catalog, COMAdminCatalogObject application)
        {
            ICatalogCollection appCatalog = (ICatalogCollection)catalog.GetCollection(COMConstants.APPLICATIONS_CATALOG_NAME);
            appCatalog.Populate();

            foreach (COMAdminCatalogObject app in appCatalog)
            {
                if (app.Key.Equals(application.Key))
                {
                    List<string> dlls = new List<string>();
                    ICatalogCollection componentCatalog = (ICatalogCollection)appCatalog.GetCollection(COMConstants.COMPONENTS_CATALOG_NAME, app.Key);
                    componentCatalog.Populate();
                    foreach (COMAdminCatalogObject comp in componentCatalog)
                    {
                        string dll = (string)comp.get_Value(@"DLL");
                        if (!dlls.Contains(dll))
                        {
                            dlls.Add(dll);
                        }
                    }

                    dlls.Sort();

                    foreach (string dllFile in dlls)
                    {
                        if (!dllFile.Equals(@"mscoree.dll", StringComparison.InvariantCultureIgnoreCase))
                        {
                            result.AppendFormat("\t<install-component file=\"{0}\" />", dllFile);
                        }
                        else
                        {
                            result.Append("\t<install-component file=\"INSERT .NET DLL FILE HERE\" net=\"true\" />");
                        }
                        result.AppendLine();
                    }
                }
            }
        }

        private void ExtractComponentProperties(StringBuilder result, ICOMAdminCatalog catalog, COMAdminCatalogObject application)
        {
            ICatalogCollection appCatalog =
                (ICatalogCollection)catalog.GetCollection(COMConstants.APPLICATIONS_CATALOG_NAME);
            appCatalog.Populate();

            foreach (COMAdminCatalogObject app in appCatalog)
            {
                if (app.Key.Equals(application.Key))
                {
                    ICatalogCollection componentCatalog = (ICatalogCollection)appCatalog.GetCollection(COMConstants.COMPONENTS_CATALOG_NAME, app.Key);
                    componentCatalog.Populate();
                    foreach (COMAdminCatalogObject comp in componentCatalog)
                    {
                        foreach (ComponentProperties property in componentsProperties)
                        {
                            try
                            {
                                object propertyValue = comp.get_Value(property.Name);
                                if (propertyValue.GetType() != property.Type)
                                {
                                    TypeConverter converter = TypeDescriptor.GetConverter(property.Type);
                                    if (converter is EnumConverter)
                                    {
                                        foreach (int value in Enum.GetValues(property.Type))
                                        {
                                            if (value == (int)propertyValue)
                                            {
                                                propertyValue = Enum.ToObject(property.Type, (int)propertyValue);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        propertyValue = converter.ConvertFrom(propertyValue);
                                    }
                                }
                                if (!propertyValue.Equals(property.DefaultValue))
                                {
                                    result.AppendFormat("\t<component-property component-name=\"{0}\" property-name=\"{1}\" value=\"{2}\" />", comp.Name, property.Name, propertyValue);
                                    result.AppendLine();
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
        }

        class ComponentProperties
        {
            public ComponentProperties(string name, Type type, object defaultValue)
            {
                Name = name;
                DefaultValue = defaultValue;
                Type = type;
            }

            public readonly string Name;
            public readonly Type Type;
            public readonly object DefaultValue;
        }
    }
}
