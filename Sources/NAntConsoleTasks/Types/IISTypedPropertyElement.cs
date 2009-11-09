using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class IISTypedPropertyElement : Element
    {
        private string propertyName;
        [TaskAttribute("name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        private string propertyValue;
        [TaskAttribute("value")]
        public string PropertyValue
        {
            get { return propertyValue; }
            set { propertyValue = value; }
        }

        private IISPropertyType propertyType = IISPropertyType.String;
        [TaskAttribute("type")]
        public IISPropertyType PropertyType
        {
            get { return propertyType; }
            set { propertyType = value; }
        }

        private readonly ArrayList multiStringEntries = new ArrayList();
        [BuildElementArray("entry", ElementType = typeof(MultiStringEntryElement))]
        public ArrayList MultiStringEntries
        {
            get
            {
                return multiStringEntries;
            }
        }
        private bool replace = false;
        [TaskAttribute("replace")]
        public bool Replace
        {
            get { return replace; }
            set { replace = value; }
        }
    }

    public class MultiStringEntryElement : Element
    {
        private string multiStringEntryValue;
        [TaskAttribute("value", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string MultiStringEntryValue
        {
            get { return multiStringEntryValue; }
            set { multiStringEntryValue = value; }
        }
    }
}
