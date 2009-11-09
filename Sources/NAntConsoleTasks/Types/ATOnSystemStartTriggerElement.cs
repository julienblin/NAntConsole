using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class ATOnSystemStartTriggerElement : ATTriggerElement
    {
        public override Trigger CreateTrigger()
        {
            OnSystemStartTrigger onSystemStartTrigger = new OnSystemStartTrigger();

            PopulateTrigger(onSystemStartTrigger);

            return onSystemStartTrigger;
        }

        public override string ToString()
        {
            return @"system start";
        }
    }
}
