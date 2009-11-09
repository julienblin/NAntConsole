using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.Configuration
{
    public class NAntPropertiesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new NAntPropertyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NAntPropertyElement)element).Name;
        }
    }
}
