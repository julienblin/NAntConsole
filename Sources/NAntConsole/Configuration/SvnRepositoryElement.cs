using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.Configuration
{
    public class SvnRepositoryElement : ConfigurationElement
    {
        [ConfigurationProperty("uri", IsRequired = true, IsKey = true)]
        public string Uri
        {
            get
            {
                return (string)this["uri"];
            }
            set
            {
                this["uri"] = value;
            }
        }
    }
}
