using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class ATOnIdleTriggerElement : ATTriggerElement
    {
        public override Trigger CreateTrigger()
        {
            OnIdleTrigger onIdleTrigger = new OnIdleTrigger();

            PopulateTrigger(onIdleTrigger);

            return onIdleTrigger;
        }

        public override string ToString()
        {
            return @"idle";
        }
    }
}
