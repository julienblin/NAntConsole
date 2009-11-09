using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Dialogs
{
    public partial class AskUserCredentialsForm : Form
    {
        public AskUserCredentialsForm()
        {
            InitializeComponent();
        }

        public string Username
        {
            get { return textBoxUsername.Text; }
            set { textBoxUsername.Text = value; }
        }

        public string Password
        {
            get { return textBoxPassword.Text; }
            set { textBoxPassword.Text = value; }
        }
        
        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == buttonOK)
                DialogResult = DialogResult.OK;

            Close();
        }
    }
}