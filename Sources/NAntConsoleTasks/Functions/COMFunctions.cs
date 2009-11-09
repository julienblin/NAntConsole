using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.COM;
using Comadmin;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Functions
{
    [FunctionSet("com", "com")]
    public class COMFunctions : FunctionSetBase
    {
        public COMFunctions(Project project, PropertyDictionary properties)
            : base(project, properties)
        {
        }

        [Function("get-component-property")]
        public static string GetComponentProperty(string applicationName, string componentName, string propertyName)
        {
            COMAdminCatalog comAdminCatalog = new COMAdminCatalog();
            try
            {
                ICatalogCollection appCatalog = (ICatalogCollection)comAdminCatalog.GetCollection(COMConstants.APPLICATIONS_CATALOG_NAME);
                appCatalog.Populate();
                foreach (COMAdminCatalogObject application in appCatalog)
                {
                    if (!application.Name.Equals(applicationName)) continue;

                    ICatalogCollection componentCatalog = (ICatalogCollection)appCatalog.GetCollection(COMConstants.COMPONENTS_CATALOG_NAME, application.Key);
                    componentCatalog.Populate();
                    foreach (COMAdminCatalogObject comComp in componentCatalog)
                    {
                        if (((string)comComp.Name).Equals(componentName, StringComparison.InvariantCulture))
                        {
                            return comComp.get_Value(propertyName).ToString();
                        }
                    }
                }

                return null;
            }
            finally
            {
                Marshal.ReleaseComObject(comAdminCatalog);
            }
        }
    }
}
