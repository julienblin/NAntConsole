using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.Configuration
{
    public class UpdaterLocationsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new UpdaterLocation();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UpdaterLocation)element).Path;
        }
    }
}
