using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class CommandExecutionResult
    {
        private readonly IUICommand command;

        public CommandExecutionResult(IUICommand command)
        {
            this.command = command;
        }

        public IUICommand Command
        {
            get { return command; }
        }

        private Exception error;

        public Exception Error
        {
            get { return error; }
            set { error = value; }
        }

        private FileInfo newProjectFile;

        public FileInfo NewProjectFile
        {
            get { return newProjectFile; }
            set { newProjectFile = value; }
        }

        private object commandOutput;

        public object CommandOutput
        {
            get { return commandOutput; }
            set { commandOutput = value; }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
