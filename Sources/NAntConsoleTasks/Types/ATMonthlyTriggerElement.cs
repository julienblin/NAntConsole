using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class ATMonthlyTriggerElement : ATStartableTriggerElement
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

        private string days;
        [TaskAttribute("days", Required = true)]
        [StringValidator(AllowEmpty = false, Expression = @"^(([0-9]{1,2})+,?\s?)+$")]
        public string Days
        {
            get { return days; }
            set { days = value; }
        }

        public override Trigger CreateTrigger()
        {
            List<int> intDays = new List<int>();
            string[] splittedDays = Days.Split(',');
            foreach (string day in splittedDays)
            {
                if(!string.IsNullOrEmpty(day.Trim()))
                intDays.Add(Convert.ToInt32(day.Trim()));
            }

            MonthlyTrigger monthlyTrigger = null;
            if (monthsSet)
            {
                monthlyTrigger = new MonthlyTrigger(StartHour, StartMinute, intDays.ToArray(), Months);
            }
            else
            {
                monthlyTrigger = new MonthlyTrigger(StartHour, StartMinute, intDays.ToArray());
            }

            PopulateTrigger(monthlyTrigger);

            return monthlyTrigger;
        }

        public override string ToString()
        {
            return string.Format("monthly : StartHour={0}, StartMinute={1}, Days={2}", StartHour, StartMinute, Days);
        }
    }
}
