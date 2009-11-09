using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class ATDailyTriggerElement : ATStartableTriggerElement
    {
        private short daysInterval = short.MinValue;
        [TaskAttribute("daysInterval")]
        public short DaysInterval
        {
            get { return daysInterval; }
            set { daysInterval = value; }
        }

        public override Trigger CreateTrigger()
        {
            DailyTrigger dailyTrigger = null;
            if (DaysInterval != short.MinValue)
            {
                dailyTrigger = new DailyTrigger(StartHour, StartMinute, DaysInterval);
            }
            else
            {
                dailyTrigger = new DailyTrigger(StartHour, StartMinute);
            }

            PopulateTrigger(dailyTrigger);

            return dailyTrigger;
        }

        public override string ToString()
        {
            return string.Format("daily : StartHour={0}, StartMinute={1}", StartHour, StartMinute);
        }
    }
}
