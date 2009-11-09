using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using System.Collections;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class IISServerBindings : Element
    {
        private readonly ArrayList entries = new ArrayList();
        [BuildElementArray("entry", ElementType = typeof(MultiStringEntryElement))]
        public ArrayList Entries
        {
            get
            {
                return entries;
            }
        }
    }
}
