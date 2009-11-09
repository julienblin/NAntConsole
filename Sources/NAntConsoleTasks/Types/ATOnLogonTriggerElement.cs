using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class ATOnLogonTriggerElement : ATTriggerElement
    {
        public override Trigger CreateTrigger()
        {
            OnLogonTrigger onLogonTrigger = new OnLogonTrigger();

            PopulateTrigger(onLogonTrigger);

            return onLogonTrigger;
        }

        public override string ToString()
        {
            return @"logon";
        }
    }
}
