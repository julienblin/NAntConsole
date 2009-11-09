using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Comadmin;
using NAnt.Core;
using NAnt.Core.Tasks;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.COM
{
    public abstract class BaseCOMTask : Task
    {
        private readonly ICOMAdminCatalog comAdminCatalog = new COMAdminCatalog();

        private readonly Dictionary<string, Type> specificProperties = new Dictionary<string, Type>();

        public Dictionary<string, Type> SpecificProperties
        {
            get { return specificProperties; }
        }

        protected ICOMAdminCatalog ComAdminCatalog
        {
            get { return comAdminCatalog; }
        }

        protected override void Initialize()
        {
            base.Initialize();
            SpecificProperties.Add("AccessChecksLevel", typeof(COMAdminAccessChecksLevelOptions));
            SpecificProperties.Add("Activation", typeof(COMAdminActivationOptions));
            SpecificProperties.Add("ApplicationProxy", typeof(bool));
            SpecificProperties.Add("Authentication", typeof(COMAdminAuthenticationLevelOptions));
            SpecificProperties.Add("AuthenticationCapability", typeof(COMAdminAuthenticationCapabilitiesOptions));
            SpecificProperties.Add("Changeable", typeof(bool));
            SpecificProperties.Add("ConcurrentApps", typeof(int));
            SpecificProperties.Add("CRMEnabled", typeof(bool));
            SpecificProperties.Add("Deleteable", typeof(bool));
            SpecificProperties.Add("DumpOnException", typeof(bool));
            SpecificProperties.Add("DumpOnFailfast", typeof(bool));
            SpecificProperties.Add("ImpersonationLevel", typeof(COMAdminImpersonationLevelOptions));
            SpecificProperties.Add("MaxDumpCount", typeof(int));
            SpecificProperties.Add("RunForever", typeof(bool));
            SpecificProperties.Add("ShutdownAfter", typeof(int));

            //Components
            SpecificProperties.Add("AllowInprocSubscribers", typeof(bool));
            SpecificProperties.Add("ComponentTransactionTimeout", typeof(int));
            SpecificProperties.Add("COMTIIntrinsics", typeof(bool));
            SpecificProperties.Add("CreationTimeout", typeof(int));
            SpecificProperties.Add("FireInParallel", typeof(bool));
            SpecificProperties.Add("IISIntrinsics", typeof(bool));
            SpecificProperties.Add("IsEventClass", typeof(bool));
            SpecificProperties.Add("JustInTimeActivation", typeof(bool));
            SpecificProperties.Add("LoadBalancingSupported", typeof(bool));
            SpecificProperties.Add("MaxPoolSize", typeof(int));
            SpecificProperties.Add("MinPoolSize", typeof(int));
            SpecificProperties.Add("MustRunInClientContext", typeof(bool));
            SpecificProperties.Add("Synchronization", typeof(COMAdminSynchronizationOptions));
            SpecificProperties.Add("ThreadingModel", typeof(COMAdminThreadingModels));
            SpecificProperties.Add("Transaction", typeof(COMAdminTransactionOptions));
        }

        protected override sealed void ExecuteTask()
        {
            try
            {
                ExecuteCOMTask();
            }
            finally
            {
                Marshal.ReleaseComObject(comAdminCatalog);
            }
        }

        protected abstract void ExecuteCOMTask();

        protected void DisposeComAdminCatalog()
        {
            Marshal.ReleaseComObject(comAdminCatalog);
        }

        protected COMSearchResult FindApplication(string name)
        {
            COMSearchResult searchResult = null;
            ICatalogCollection appCatalog = (ICatalogCollection)ComAdminCatalog.GetCollection(COMConstants.APPLICATIONS_CATALOG_NAME);
            appCatalog.Populate();
            int index = 0;
            foreach (COMAdminCatalogObject application in appCatalog)
            {
                if (application.Name.Equals(name))
                {
                    searchResult = new COMSearchResult(appCatalog, application, index);
                }
                ++index;
            }

            if (searchResult != null)
            {
                return searchResult;
            }
            else
            {
                return new COMSearchResult(appCatalog, true);
            }
        }

        protected COMSearchResult FindComponent(string applicationName, string componentName)
        {
            COMSearchResult searchResult = null;
            ICatalogCollection appCatalog = (ICatalogCollection)ComAdminCatalog.GetCollection(COMConstants.APPLICATIONS_CATALOG_NAME);
            appCatalog.Populate();
            foreach (COMAdminCatalogObject application in appCatalog)
            {
                if (!application.Name.Equals(applicationName)) continue;

                ICatalogCollection componentCatalog = (ICatalogCollection)appCatalog.GetCollection(COMConstants.COMPONENTS_CATALOG_NAME, application.Key);
                componentCatalog.Populate();
                int index = 0;
                foreach (COMAdminCatalogObject comComp in componentCatalog)
                {
                    if (((string)comComp.Name).Equals(componentName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        searchResult = new COMSearchResult(componentCatalog, comComp, index);
                    }
                }
                ++index;

                Marshal.ReleaseComObject(appCatalog);
                if (searchResult != null)
                {
                    return searchResult;
                }
                else
                {
                    return new COMSearchResult(componentCatalog, true);
                }
            }

            return new COMSearchResult(appCatalog, true);
        }

        protected void ApplyProperties(ArrayList properties, ICatalogObject catalogObject)
        {
            foreach (PropertyTask propertyTask in properties)
            {
                string value;
                if (!propertyTask.Dynamic)
                {
                    value = Project.ExpandProperties(propertyTask.Value, Location);
                }
                else
                {
                    value = propertyTask.Value;
                }
                ApplyProperty(propertyTask.PropertyName, value, catalogObject);
            }
        }

        protected void ApplyProperty(string name, string value, ICatalogObject catalogObject)
        {
            Log(Level.Info, Resources.BaseCOMTaskApplyProperty, name, value);
            if (SpecificProperties.ContainsKey(name))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(SpecificProperties[name]);
                try
                {
                    object convertedValue = converter.ConvertFromString(value);
                    catalogObject.set_Value(name, convertedValue);
                }
                catch (FormatException)
                {
                    StringBuilder sbMessage = new StringBuilder();
                    sbMessage.AppendFormat(Resources.BaseCOMTaskFormatPossibleValues, value, name);
                    sbMessage.AppendLine();
                    if (converter is EnumConverter)
                    {
                        foreach (string possibleValue in Enum.GetNames(SpecificProperties[name]))
                        {
                            sbMessage.AppendLine(possibleValue);
                        }
                    }

                    if (converter is Int32Converter)
                    {
                        sbMessage.AppendLine("any number");
                    }

                    throw new BuildException(sbMessage.ToString());
                }
            }
            else
            {
                // Hack asserting that if the property ends with Enabled, then it's a boolean.
                if (name.EndsWith("Enabled", StringComparison.InvariantCulture))
                {
                    catalogObject.set_Value(name, Convert.ToBoolean(value));
                }
                else
                {
                    catalogObject.set_Value(name, value);
                }
            }
        }

        protected sealed class COMSearchResult : IDisposable
        {
            private bool disposed = false;

            public COMSearchResult(ICatalogCollection catalogCollection, ICatalogObject catalogObject, int index)
            {
                this.catalogCollection = catalogCollection;
                this.index = index;
                this.catalogObject = catalogObject;
            }

            public COMSearchResult(ICatalogCollection catalogCollection, bool notFound)
            {
                this.catalogCollection = catalogCollection;
                this.notFound = notFound;
            }

            private readonly ICatalogCollection catalogCollection;

            public ICatalogCollection CatalogCollection
            {
                get { return catalogCollection; }
            }

            private readonly ICatalogObject catalogObject;

            public ICatalogObject CatalogObject
            {
                get { return catalogObject; }
            }

            private readonly int index = -1;

            public int Index
            {
                get { return index; }
            }

            private readonly bool notFound;

            public bool NotFound
            {
                get { return notFound; }
            }

            public void Dispose()
            {
                if (!disposed)
                {
                    if (catalogObject != null) Marshal.ReleaseComObject(catalogObject);
                    if (catalogCollection != null) Marshal.ReleaseComObject(catalogCollection);
                    disposed = true;
                }
            }
        }
    }
}
