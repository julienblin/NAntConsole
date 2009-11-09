using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.Configuration
{
    public class SvnRepositoriesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SvnRepositoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SvnRepositoryElement)element).Uri;
        }
    }
}
