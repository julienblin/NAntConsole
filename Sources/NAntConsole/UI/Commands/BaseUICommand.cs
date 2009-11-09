using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal abstract class BaseUICommand : IUICommand
    {
        private string commandName;

        protected BaseUICommand(string commandName)
        {
            this.commandName = commandName;
        }

        public string CommandName
        {
            get { return commandName; }
        }

        public abstract CommandExecutionResult Execute();

        public event EventHandler<IUICommandReportProgressEventArgs> ReportProgress;

        protected void InvokeReportProgress(string message)
        {
            EventHandler<IUICommandReportProgressEventArgs> reportProgressHandler = ReportProgress;
            if (reportProgressHandler != null)
            {
                IUICommandReportProgressEventArgs eventArgs = new IUICommandReportProgressEventArgs(message);
                reportProgressHandler(this, eventArgs);
            }
        }
    }
}
