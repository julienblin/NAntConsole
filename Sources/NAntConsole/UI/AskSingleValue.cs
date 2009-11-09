using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class AskSingleValue : Form
    {
        public AskSingleValue()
        {
            InitializeComponent();
            ConnectEvents();
        }

        public string Prefix
        {
            get { return labelPrefix.Text; }
            set { labelPrefix.Text = value; }
        }

        public string Value
        {
            get { return textBoxValue.Text; }
            set { textBoxValue.Text = value; }
        }

        private void ConnectEvents()
        {
            buttonOk.Click += OnButtonOkClick;
        }

        private void OnButtonOkClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}