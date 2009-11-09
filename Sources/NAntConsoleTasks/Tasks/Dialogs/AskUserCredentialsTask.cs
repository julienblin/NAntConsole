using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core.Attributes;
using NAnt.Core;
using System.Windows.Forms;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Dialogs
{
    [TaskName("ask-user-credentials")]
    public class AskUserCredentialsTask : Task
    {
        private string propertyName;
        [TaskAttribute("property-name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        private string propertyPassword;
        [TaskAttribute("property-password", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string PropertyPassword
        {
            get { return propertyPassword; }
            set { propertyPassword = value; }
        }

        protected override void ExecuteTask()
        {
            AskUserCredentialsForm userForm = new AskUserCredentialsForm();
                        
            if (userForm.ShowDialog() == DialogResult.OK)
            {
                Properties[PropertyName] = userForm.Username;
                Properties[PropertyPassword] = userForm.Password;
            }
            else
            {
                throw new BuildException("User cancelled");
            }
        }
    }
}
