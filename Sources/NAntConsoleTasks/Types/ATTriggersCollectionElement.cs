using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class ATTriggersCollectionElement : Element
    {
        private readonly ArrayList dailyTriggers = new ArrayList();
        [BuildElementArray("daily", ElementType = typeof(ATDailyTriggerElement))]
        public ArrayList DailyTriggers
        {
            get
            {
                return dailyTriggers;
            }
        }

        private readonly ArrayList monthlyDOWTriggers = new ArrayList();
        [BuildElementArray("monthlyDOW", ElementType = typeof(ATMonthlyDOWTriggerElement))]
        public ArrayList MonthlyDOWTriggers
        {
            get
            {
                return monthlyDOWTriggers;
            }
        }

        private readonly ArrayList monthlyTriggers = new ArrayList();
        [BuildElementArray("monthly", ElementType = typeof(ATMonthlyTriggerElement))]
        public ArrayList MonthlyTriggers
        {
            get
            {
                return monthlyTriggers;
            }
        }

        private readonly ArrayList runOnceTriggers = new ArrayList();
        [BuildElementArray("once", ElementType = typeof(ATRunOnceTriggerElement))]
        public ArrayList RunOnceTriggers
        {
            get
            {
                return runOnceTriggers;
            }
        }

        private readonly ArrayList weeklyTriggers = new ArrayList();
        [BuildElementArray("weekly", ElementType = typeof(ATWeeklyTriggerElement))]
        public ArrayList WeeklyTriggers
        {
            get
            {
                return weeklyTriggers;
            }
        }

        private readonly ArrayList idleTriggers = new ArrayList();
        [BuildElementArray("idle", ElementType = typeof(ATOnIdleTriggerElement))]
        public ArrayList IdleTriggers
        {
            get
            {
                return idleTriggers;
            }
        }

        private readonly ArrayList onLogonTriggers = new ArrayList();
        [BuildElementArray("logon", ElementType = typeof(ATOnLogonTriggerElement))]
        public ArrayList OnLogonTriggers
        {
            get
            {
                return onLogonTriggers;
            }
        }

        private readonly ArrayList onSystemStartTriggers = new ArrayList();
        [BuildElementArray("systemstart", ElementType = typeof(ATOnSystemStartTriggerElement))]
        public ArrayList OnSystemStartTriggers
        {
            get
            {
                return onSystemStartTriggers;
            }
        }

        public IList<ATTriggerElement> GetTriggers()
        {
            List<ATTriggerElement> result = new List<ATTriggerElement>();

            foreach (ATTriggerElement trigger in DailyTriggers)
            {
                result.Add(trigger);
            }

            foreach (ATTriggerElement trigger in MonthlyDOWTriggers)
            {
                result.Add(trigger);
            }

            foreach (ATTriggerElement trigger in MonthlyTriggers)
            {
                result.Add(trigger);
            }

            foreach (ATTriggerElement trigger in RunOnceTriggers)
            {
                result.Add(trigger);
            }

            foreach (ATTriggerElement trigger in WeeklyTriggers)
            {
                result.Add(trigger);
            }

            foreach (ATTriggerElement trigger in IdleTriggers)
            {
                result.Add(trigger);
            }

            foreach (ATTriggerElement trigger in OnLogonTriggers)
            {
                result.Add(trigger);
            }

            foreach (ATTriggerElement trigger in OnSystemStartTriggers)
            {
                result.Add(trigger);
            }

            return result;
        }
    }
}
