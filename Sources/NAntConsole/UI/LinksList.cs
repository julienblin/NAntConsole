using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class LinksList : Form
    {
        public LinksList()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        public string Prefix
        {
            get { return labelPrefix.Text; }
            set { labelPrefix.Text = value; }
        }

        public string Project
        {
            get { return labelProject.Text; }
            set { labelProject.Text = value; }
        }

        public void AddLink(string url, string name)
        {
            ListViewItem item = new ListViewItem(new string[] { url, name });
            listViewLinks.Items.Add(item);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Auto-size columns
            columnHeaderUrl.Width = -2;
            columnHeaderName.Width = -2;
        }

        private void ConnectEventHandlers()
        {
            buttonOK.Click += OnButtonOKClick;
        }

        private void OnButtonOKClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}