using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class AddLinkSelection : Form
    {
        public AddLinkSelection()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        private void ConnectEventHandlers()
        {
            buttonOK.Click += OnButtonOkClick;
        }

        public bool BinLinkChecked
        {
            get { return checkBoxBin.Checked; }
            set { checkBoxBin.Checked = value; }
        }

        public bool EnvLinkChecked
        {
            get { return checkBoxEnv.Checked; }
            set { checkBoxEnv.Checked = value; }
        }

        private void OnButtonOkClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}