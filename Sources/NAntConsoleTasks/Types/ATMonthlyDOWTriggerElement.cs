using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class ATMonthlyDOWTriggerElement : ATStartableTriggerElement
    {
        private bool monthsSet = false;

        private MonthsOfTheYear months;
        [TaskAttribute("months")]
        public MonthsOfTheYear Months
        {
            get { return months; }
            set
            {
                months = value;
                monthsSet = true;
            }
        }

        private DaysOfTheWeek weekDays;
        [TaskAttribute("weekDays", Required = true)]
        public DaysOfTheWeek WeekDays
        {
            get { return weekDays; }
            set { weekDays = value; }
        }

        private WhichWeek whichWeeks;
        [TaskAttribute("whichWeeks", Required = true)]
        public WhichWeek WhichWeeks
        {
            get { return whichWeeks; }
            set { whichWeeks = value; }
        }

        public override Trigger CreateTrigger()
        {
            MonthlyDOWTrigger monthlyDOWTrigger = null;
            if (monthsSet)
            {
                monthlyDOWTrigger = new MonthlyDOWTrigger(StartHour, StartMinute, WeekDays, WhichWeeks, Months);
            }
            else
            {
                monthlyDOWTrigger = new MonthlyDOWTrigger(StartHour, StartMinute, WeekDays, WhichWeeks);
            }

            PopulateTrigger(monthlyDOWTrigger);

            return monthlyDOWTrigger;
        }

        public override string ToString()
        {
            return string.Format("monthlyDOW : StartHour={0}, StartMinute={1}, WeekDays={2}, WhichWeeks={3}", StartHour, StartMinute, WeekDays, WhichWeeks);
        }
    }
}
