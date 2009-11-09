using System;

namespace CDS.Framework.Tools.NAntConsole.Helpers
{
    public class NAntExecutionProgressEventArgs : EventArgs
    {
        private readonly string message;

        public NAntExecutionProgressEventArgs(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get { return message; }
        }
    }
}