using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class ATRunOnceTriggerElement : ATTriggerElement
    {
        private DateTime runDateTime;
        [TaskAttribute("date", Required = true)]
        public DateTime RunDateTime
        {
            get { return runDateTime; }
            set { runDateTime = value; }
        }

        public override Trigger CreateTrigger()
        {
            RunOnceTrigger runOnceTrigger = new RunOnceTrigger(runDateTime);

            PopulateTrigger(runOnceTrigger);

            return runOnceTrigger;
        }

        public override string ToString()
        {
            return string.Format("once : Date={0:yyyyMMdd - HH:mm:ss}", RunDateTime);
        }
    }
}
