using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.SVN
{
    public abstract class BaseSVNMessageTask : BaseSVNTask
    {
        private string message;
        [TaskAttribute("message", Required = true)]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
