using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class COMComponentPropertyElement : Element
    {
        private string componentName;
        [TaskAttribute("component-name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string ComponentName
        {
            get { return componentName; }
            set { componentName = value; }
        }

        private string propertyName;
        [TaskAttribute("property-name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        private string propertyValue;
        [TaskAttribute("value", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string PropertyValue
        {
            get { return propertyValue; }
            set { propertyValue = value; }
        }
    }
}
