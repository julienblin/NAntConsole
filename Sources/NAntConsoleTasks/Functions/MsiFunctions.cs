using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using Microsoft.Win32;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Functions
{
    [FunctionSet("msi", "msi")]
    public class MsiFunctions : FunctionSetBase
    {
        const string BASE_INSTALLER_PRODUCT_KEY = @"Installer\Products";
        const string PACKAGE_CODE_VALUE_NAME = @"PackageCode";

        public MsiFunctions(Project project, PropertyDictionary properties)
            : base(project, properties)
        {
        }

        [Function("is-product-installed")]
        public static bool IsProductInstalled(string productCode)
        {
            RegistryKey baseProducts = Registry.ClassesRoot.OpenSubKey(BASE_INSTALLER_PRODUCT_KEY);
            string[] subKeyNames = baseProducts.GetSubKeyNames();
            foreach (string subKeyName in subKeyNames)
            {
                string packageCodeValue = (string)baseProducts.OpenSubKey(subKeyName).GetValue(PACKAGE_CODE_VALUE_NAME);
                if (!string.IsNullOrEmpty(packageCodeValue))
                {
                    if (packageCodeValue.Equals(productCode, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
            }
            return false;
        }
    }
}
