using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Configuration;
using CDS.Framework.Tools.NAntConsole.Helpers;
using SharpSvn;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class SvnExplorer : Form
    {
        static Regex reStripRootUri = new Regex(@".+/(?<lastPart>[^/]+)/?", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);
        static Regex reTrunkSubTagOrSubBranchOnly = new Regex(@"^.+/(trunk|branches/[^/]+|tags/[^/]+)/?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        public SvnExplorer()
        {
            InitializeComponent();
            ConnectEvents();
        }

        public SvnExplorerSelection GetSvnExplorerSelection()
        {
            if ((treeViewFolders.SelectedNode != null) && (comboBoxRepository.SelectedItem != null))
            {
                return new SvnExplorerSelection((string)comboBoxRepository.SelectedItem, (string)treeViewFolders.SelectedNode.Tag);
            }
            else
            {
                return null;
            }
        }

        private bool readOnly = true;

        public bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
        }

        private bool trunkSubTagOrSubBranchOnly;

        public bool TrunkSubTagOrSubBranchOnly
        {
            get { return trunkSubTagOrSubBranchOnly; }
            set { trunkSubTagOrSubBranchOnly = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!ReadOnly)
            {
                treeViewFolders.ContextMenuStrip = contextMenuStrip;
            }

            NAntConsoleConfigurationSection configurationSection =
                NAntConsoleConfigurationSection.GetConfigurationSection();

            foreach (SvnRepositoryElement svnRepositoryElement in configurationSection.SvnRepositories)
            {
                comboBoxRepository.Items.Add(svnRepositoryElement.Uri);
            }
            comboBoxRepository.SelectedIndex = 0;
        }

        private void ConnectEvents()
        {
            comboBoxRepository.SelectedValueChanged += OnComboBoxRepositorySelectedValueChanged;
            treeViewFolders.AfterSelect += OnTreeViewFoldersAfterSelect;
            backgroundWorker.DoWork += OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            buttonOk.Click += OnButtonOkClick;
            toolStripMenuItemCreateFolder.Click += OnToolStripMenuItemCreateFolderClick;
            toolStripMenuItemCreateBTT.Click += OnToolStripMenuItemCreateBTTClick;
        }

        private void OnComboBoxRepositorySelectedValueChanged(object sender, EventArgs e)
        {
            treeViewFolders.Nodes.Clear();
            string rootUri = (string)comboBoxRepository.SelectedItem;
            string lastPart = reStripRootUri.Match(rootUri).Groups["lastPart"].Value;
            TreeNode rootNode = CreateTreeNode(lastPart, rootUri.EndsWith("/") ? rootUri : string.Concat(rootUri, "/"));
            treeViewFolders.Nodes.Add(rootNode);
            treeViewFolders.SelectedNode = rootNode;
        }

        private static TreeNode CreateTreeNode(string text, string uri)
        {
            TreeNode node = new TreeNode(text);
            node.Tag = uri;
            return node;
        }

        private void OnTreeViewFoldersAfterSelect(object sender, TreeViewEventArgs e)
        {
            string svnUri = (string)e.Node.Tag;
            LoadSvnUriAndAttachToSelectedNode(svnUri);
        }

        private void LoadSvnUriAndAttachToSelectedNode(string svnUri)
        {
            toolStripStatusLabel.Text = string.Format(Resources.SvnExplorerLoading, svnUri);
            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
            this.Enabled = false;
            backgroundWorker.RunWorkerAsync(svnUri);
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SvnHelper.GetChildrenInfo((string)e.Argument);
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel.Text = string.Format(Resources.SvnExplorerLoaded, treeViewFolders.SelectedNode.Tag);
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
            this.Enabled = true;

            treeViewFolders.SelectedNode.Nodes.Clear();
            treeViewFolders.ExpandAll();
            IEnumerable<SvnInfoEventArgs> svnInfoEventArgs = (IEnumerable<SvnInfoEventArgs>)e.Result;
            foreach (SvnInfoEventArgs infoEventArg in svnInfoEventArgs)
            {
                if (infoEventArg.NodeKind == SvnNodeKind.Directory)
                {
                    TreeNode node = CreateTreeNode(infoEventArg.Path, infoEventArg.Uri.ToString());
                    treeViewFolders.SelectedNode.Nodes.Add(node);
                }
            }

            treeViewFolders.SelectedNode.ExpandAll();
            treeViewFolders.Focus();

            buttonOk.Enabled = ButtonOkEnabledBasedOnSelectedNode();
        }

        private bool ButtonOkEnabledBasedOnSelectedNode()
        {
            if (treeViewFolders.SelectedNode == null)
            {
                return false;
            }

            if (TrunkSubTagOrSubBranchOnly)
            {
                string selectedSvnUri = (string) treeViewFolders.SelectedNode.Tag;
                return reTrunkSubTagOrSubBranchOnly.Match(selectedSvnUri).Success;
            }
            else
            {
                return true;
            }
        }

        private void OnButtonOkClick(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void OnToolStripMenuItemCreateFolderClick(object sender, EventArgs e)
        {
            if (treeViewFolders.SelectedNode != null)
            {
                string svnBaseUri = (string)treeViewFolders.SelectedNode.Tag;
                AskSingleValue newFolderName = new AskSingleValue();
                newFolderName.Text = Resources.NewFolder;
                newFolderName.Prefix = Resources.FolderName;
                if (newFolderName.ShowDialog(this) == DialogResult.OK)
                {
                    SvnHelper.CreateFolder(svnBaseUri, newFolderName.Value);
                    LoadSvnUriAndAttachToSelectedNode(svnBaseUri);
                }
            }
        }

        private void OnToolStripMenuItemCreateBTTClick(object sender, EventArgs e)
        {
            if (treeViewFolders.SelectedNode != null)
            {
                string svnBaseUri = (string)treeViewFolders.SelectedNode.Tag;
                SvnHelper.CreateFolder(svnBaseUri, "branches");
                SvnHelper.CreateFolder(svnBaseUri, "tags");
                SvnHelper.CreateFolder(svnBaseUri, "trunk");
                LoadSvnUriAndAttachToSelectedNode(svnBaseUri);
            }
        }
    }
}