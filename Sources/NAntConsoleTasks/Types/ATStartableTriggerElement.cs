using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public abstract class ATStartableTriggerElement : ATTriggerElement
    {
        private short startHour;
        [TaskAttribute("startHour", Required = true)]
        public short StartHour
        {
            get { return startHour; }
            set { startHour = value; }
        }

        private short startMinute;
        [TaskAttribute("startMinute", Required = true)]
        public short StartMinute
        {
            get { return startMinute; }
            set { startMinute = value; }
        }
    }
}
