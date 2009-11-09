using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.Helpers
{
    internal class SvnExecutionProgressEventArgs
    {
        private readonly string message;

        public SvnExecutionProgressEventArgs(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get { return message; }
        }
    }
}
