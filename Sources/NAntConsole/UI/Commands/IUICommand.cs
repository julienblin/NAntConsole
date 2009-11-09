using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal interface IUICommand
    {
        string CommandName
        {
            get;
        }

        CommandExecutionResult Execute();

        event EventHandler<IUICommandReportProgressEventArgs> ReportProgress;
    }

    internal class IUICommandReportProgressEventArgs : EventArgs
    {
        private readonly string message;

        public IUICommandReportProgressEventArgs(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get { return message; }
        }
    }
}
