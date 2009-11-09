using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class ATWeeklyTriggerElement : ATStartableTriggerElement
    {
        private DaysOfTheWeek weekDays;
        [TaskAttribute("weekDays", Required = true)]
        public DaysOfTheWeek WeekDays
        {
            get { return weekDays; }
            set { weekDays = value; }
        }

        private short weeksInterval = short.MinValue;
        [TaskAttribute("weeksInterval")]
        public short WeeksInterval
        {
            get { return weeksInterval; }
            set { weeksInterval = value; }
        }

        public override Trigger CreateTrigger()
        {
            WeeklyTrigger weeklyTrigger = null;
            if (WeeksInterval != short.MinValue)
            {
                weeklyTrigger = new WeeklyTrigger(StartHour, StartMinute, WeekDays, WeeksInterval);
            }
            else
            {
                weeklyTrigger = new WeeklyTrigger(StartHour, StartMinute, WeekDays);
            }

            PopulateTrigger(weeklyTrigger);

            return weeklyTrigger;
        }

        public override string ToString()
        {
            return string.Format("weekly : StartHour={0}, StartMinute={1}, WeekDays={2}", StartHour, StartMinute, WeekDays);
        }
    }
}
