using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class IISMetabaseExtractor : Form
    {
        const string IIS_ADSI_ROOT = @"IIS://{0}/W3SVC";
        static List<string> allowedSchemaClass;

        IISMetabaseDumper dumper = new IISMetabaseDumper();

        public IISMetabaseExtractor()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        private string ServerName
        {
            get { return textBoxServerName.Text; }
            set { textBoxServerName.Text = value; }
        }

        private void ConnectEventHandlers()
        {
            buttonConnect.Click += OnButtonConnectClick;
            buttonOK.Click += OnButtonOKClick;
            buttonCopyToClipboard.Click += OnButtonCopyToClipboardClick;
            treeViewMetabase.AfterSelect += OnTreeViewMetabaseAfterSelect;
        }

        private void OnButtonConnectClick(object sender, EventArgs e)
        {
            try
            {
                LoadServerMetabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnButtonOKClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnButtonCopyToClipboardClick(object sender, EventArgs e)
        {
           Clipboard.SetText(textBoxCopyInformation.Text);
        }

        private void OnTreeViewMetabaseAfterSelect(object sender, TreeViewEventArgs e)
        {
            textBoxCopyInformation.Clear();
            DirectoryEntry selectedDirEntry = new DirectoryEntry((string)e.Node.Tag);
            switch (selectedDirEntry.SchemaClassName)
            {
                case IISConstants.TYPE_WEBSERVER:
                    textBoxCopyInformation.Text = dumper.DumpWebsite(selectedDirEntry);
                    break;
                case IISConstants.TYPE_VDIR:
                    textBoxCopyInformation.Text = dumper.DumpVDir(selectedDirEntry);
                    break;
            }
        }

        private void LoadServerMetabase()
        {
            treeViewMetabase.Nodes.Clear();
            DirectoryEntry rootDirectoryEntry = new DirectoryEntry(string.Format(IIS_ADSI_ROOT, ServerName));
            treeViewMetabase.Nodes.Add(CreateTreeNode(rootDirectoryEntry));
            treeViewMetabase.Nodes[0].Expand();
        }

        private static TreeNode CreateTreeNode(DirectoryEntry directoryEntry)
        {
            InitAllowedSchemaClass();
            if (allowedSchemaClass.Contains(directoryEntry.SchemaClassName))
            {
                TreeNode node = new TreeNode();
                if (directoryEntry.SchemaClassName.Equals(IISConstants.TYPE_WEBSERVER, StringComparison.InvariantCulture))
                {
                    node.Text = IISConstants.GetProperty<string>(directoryEntry, IISConstants.PROPERTY_WEBSITE_NAME);
                }
                else
                {
                    node.Text = directoryEntry.Name;
                }
                node.ImageKey = directoryEntry.SchemaClassName;
                node.SelectedImageKey = directoryEntry.SchemaClassName;
                node.Tag = directoryEntry.Path;

                foreach (DirectoryEntry child in directoryEntry.Children)
                {
                    TreeNode childNode = CreateTreeNode(child);
                    if (childNode != null) node.Nodes.Add(childNode);
                }
                return node;
            }
            else
            {
                return null;
            }
        }

        private static void InitAllowedSchemaClass()
        {
            if (allowedSchemaClass == null)
            {
                allowedSchemaClass = new List<string>();
                allowedSchemaClass.Add(IISConstants.TYPE_WEBSERVICE);
                allowedSchemaClass.Add(IISConstants.TYPE_WEBSERVER);
                allowedSchemaClass.Add(IISConstants.TYPE_VDIR);
            }
        }
    }
}