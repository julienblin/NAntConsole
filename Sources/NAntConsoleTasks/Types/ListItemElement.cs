using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class ListItemElement : Element
    {
        private string itemValue;
        [TaskAttribute("value", Required = true)]
        public string ItemValue
        {
            get { return itemValue; }
            set { itemValue = value; }
        }
    }
}
