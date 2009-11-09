using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using System.Windows.Forms;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Dialogs
{
    [TaskName("ask-user")]
    public class AskUserTask : Task
    {
        private string propertyMessage;
        [TaskAttribute("property", Required=true)]
        [StringValidator(AllowEmpty = false)]
        public string PropertyMessage
        {
            get { return propertyMessage; }
            set { propertyMessage = value; }
        }
        private string message;
        [TaskAttribute("message",Required=true)]
        [StringValidator(AllowEmpty=false)]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        protected override void ExecuteTask()
        {
            AskUserForm userForm = new AskUserForm();
            userForm.Message = Message;
            if (userForm.ShowDialog() == DialogResult.OK)
            {
                Properties[PropertyMessage] = userForm.Value;
            }
            else
            {
                throw new BuildException("User cancelled");
            }
        }
    }
}
