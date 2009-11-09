using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.COM;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS;
using Comadmin;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class COMComponentsExtractor : Form
    {
        private const string IMAGE_LIBRARY = @"COMLibrary";
        private const string IMAGE_SERVER = @"COMServer";
        private const string IMAGE_COMPONENT = @"COMComponent";

        public COMComponentsExtractor()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        private ICOMAdminCatalog comAdminCatalog;
        COMComponentsDumper dumper = new COMComponentsDumper();

        private string ServerName
        {
            get { return textBoxServerName.Text; }
            set { textBoxServerName.Text = value; }
        }

        private void ConnectEventHandlers()
        {
            Disposed += OnDisposed;
            buttonConnect.Click += OnButtonConnectClick;
            buttonOK.Click += OnButtonOKClick;
            buttonCopyToClipboard.Click += OnButtonCopyToClipboardClick;
            treeViewComponents.AfterSelect += OnTreeViewMetabaseAfterSelect;
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            Marshal.ReleaseComObject(comAdminCatalog);
        }

        private void OnButtonConnectClick(object sender, EventArgs e)
        {
            try
            {
                comAdminCatalog = new COMAdminCatalog();
                comAdminCatalog.Connect(textBoxServerName.Text);
                LoadServerComponents();
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
            TreeNode selectedNode = e.Node;

            if (selectedNode.ImageKey.Equals(IMAGE_SERVER) || selectedNode.ImageKey.Equals(IMAGE_LIBRARY))
            {
                textBoxCopyInformation.Text = dumper.DumpApplication(comAdminCatalog, (COMAdminCatalogObject) selectedNode.Tag);
            }
        }

        private void LoadServerComponents()
        {
            treeViewComponents.Nodes.Clear();
            TreeNode rootNode = new TreeNode(ServerName);
            rootNode.ImageIndex = 0;
            rootNode.SelectedImageIndex = 0;
            treeViewComponents.Nodes.Add(rootNode);

            List<COMAdminCatalogObject> apps = GetApplications();

            foreach (COMAdminCatalogObject application in apps)
            {
                try
                {
                    COMAdminActivationOptions activationOption = (COMAdminActivationOptions) application.get_Value("Activation");
                    TreeNode appNode = new TreeNode((string)application.Name);

                    switch (activationOption)
                    {
                        case COMAdminActivationOptions.COMAdminActivationInproc:
                            appNode.ImageKey = IMAGE_LIBRARY;
                            appNode.SelectedImageKey = IMAGE_LIBRARY;
                            break;
                        case COMAdminActivationOptions.COMAdminActivationLocal:
                            appNode.ImageKey = IMAGE_SERVER;
                            appNode.SelectedImageKey = IMAGE_SERVER;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    appNode.Tag = application;

                    List<COMAdminCatalogObject> appComponents = GetComponents((string) application.Key);
                    foreach (COMAdminCatalogObject comp in appComponents)
                    {
                        TreeNode compNode = new TreeNode((string)comp.Name);
                        compNode.ImageKey = IMAGE_COMPONENT;
                        compNode.SelectedImageKey = IMAGE_COMPONENT;
                        appNode.Nodes.Add(compNode);
                    }

                    rootNode.Nodes.Add(appNode);
                }
                catch
                {
                    
                }
            }

            rootNode.Expand();
        }

        private List<COMAdminCatalogObject> GetApplications()
        {
            List<COMAdminCatalogObject> result = new List<COMAdminCatalogObject>();

            ICatalogCollection appCatalog = (ICatalogCollection)comAdminCatalog.GetCollection(COMConstants.APPLICATIONS_CATALOG_NAME);
            appCatalog.Populate();

            foreach (COMAdminCatalogObject application in appCatalog)
            {
                result.Add(application);
            }

            result.Sort(new Comparison<COMAdminCatalogObject>(delegate(COMAdminCatalogObject x, COMAdminCatalogObject y)
            {
                return ((string)x.Name).CompareTo((string)y.Name);
            }));

            return result;
        }

        private List<COMAdminCatalogObject> GetComponents(string appKey)
        {
            List<COMAdminCatalogObject> result = new List<COMAdminCatalogObject>();

            ICatalogCollection appCatalog = (ICatalogCollection)comAdminCatalog.GetCollection(COMConstants.APPLICATIONS_CATALOG_NAME);
            appCatalog.Populate();

            foreach (COMAdminCatalogObject application in appCatalog)
            {
                if (application.Key.Equals(appKey))
                {
                    ICatalogCollection componentCatalog = (ICatalogCollection)appCatalog.GetCollection(COMConstants.COMPONENTS_CATALOG_NAME, appKey);
                    componentCatalog.Populate();
                    foreach (COMAdminCatalogObject comComp in componentCatalog)
                    {
                        result.Add(comComp);
                    }
                }
            }

            result.Sort(new Comparison<COMAdminCatalogObject>(delegate(COMAdminCatalogObject x, COMAdminCatalogObject y)
            {
                return ((string)x.Name).CompareTo((string)y.Name);
            }));

            return result;
        }
    }
}