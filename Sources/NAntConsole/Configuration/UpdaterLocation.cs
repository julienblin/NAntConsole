using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.Configuration
{
    public class UpdaterLocation : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true, IsKey = true)]
        public string Path
        {
            get
            {
                return (string)this["path"];
            }
            set
            {
                this["path"] = value;
            }
        }
    }
}
