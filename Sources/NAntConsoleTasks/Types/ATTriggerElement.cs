using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using TaskScheduler;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public abstract class ATTriggerElement : Element
    {
        private DateTime beginDate = DateTime.MinValue;
        [TaskAttribute("beginDate")]
        public DateTime BeginDate
        {
            get { return beginDate; }
            set { beginDate = value; }
        }

        private bool disabled;
        [TaskAttribute("disabled")]
        public bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }

        private int durationMinutes = Int32.MinValue;
        [TaskAttribute("durationMinutes")]
        public int DurationMinutes
        {
            get { return durationMinutes; }
            set { durationMinutes = value; }
        }

        private DateTime endDate = DateTime.MinValue;
        [TaskAttribute("endDate")]
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        private int intervalMinutes = Int32.MinValue;
        [TaskAttribute("intervalMinutes")]
        public int IntervalMinutes
        {
            get { return intervalMinutes; }
            set { intervalMinutes = value; }
        }

        private bool killAtDurationEnd;
        [TaskAttribute("killAtDurationEnd")]
        public bool KillAtDurationEnd
        {
            get { return killAtDurationEnd; }
            set { killAtDurationEnd = value; }
        }

        protected void PopulateTrigger(Trigger trigger)
        {
            if (trigger == null) throw new ArgumentNullException("trigger");

            if (BeginDate != DateTime.MinValue)
                trigger.BeginDate = BeginDate;

            trigger.Disabled = Disabled;

            if (DurationMinutes != Int32.MinValue)
                trigger.DurationMinutes = DurationMinutes;

            if (EndDate != DateTime.MinValue)
                trigger.EndDate = EndDate;

            if (IntervalMinutes != Int32.MinValue)
                trigger.IntervalMinutes = IntervalMinutes;

            trigger.KillAtDurationEnd = KillAtDurationEnd;
        }

        public abstract Trigger CreateTrigger();
    }
}
