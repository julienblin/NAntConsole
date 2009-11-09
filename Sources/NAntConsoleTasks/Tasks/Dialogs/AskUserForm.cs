using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Dialogs
{
    public partial class AskUserForm : Form
    {
        public string Message
        {
            get { return labelMessage.Text; }
            set { labelMessage.Text = value; }
        }

        public string Value
        {
            get { return textboxValue.Text; }
            set { textboxValue.Text = value; }
        }

        public AskUserForm()
        {
           InitializeComponent();
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == buttonOk)
                DialogResult = DialogResult.OK;

            Close();
        }
    }
}