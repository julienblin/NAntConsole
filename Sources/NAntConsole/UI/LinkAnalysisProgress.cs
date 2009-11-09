using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Helpers;
using SharpSvn;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class LinkAnalysisProgress : Form
    {
        public LinkAnalysisProgress()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        private void ConnectEventHandlers()
        {
            buttonStop.Click += OnButtonStopClick;
            backgroundWorker.DoWork += OnBackgroundWorkerDoWork;
            backgroundWorker.ProgressChanged += OnBackgroundWorkerProgressChanged;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
        }

        private SvnExplorerSelection svnExplorerSelection;

        public SvnExplorerSelection SvnExplorerSelection
        {
            get { return svnExplorerSelection; }
            set { svnExplorerSelection = value; }
        }

        private string searchProjectUri;

        public string SearchProjectUri
        {
            get { return searchProjectUri; }
            set { searchProjectUri = value; }
        }

        private List<string> result;

        public List<string> Result
        {
            get { return result; }
            set { result = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            labelProgress.Text = string.Format(Resources.Scanning, SvnExplorerSelection.SvnUri);
            backgroundWorker.RunWorkerAsync(new KeyValuePair<string, SvnExplorerSelection>(SearchProjectUri, SvnExplorerSelection));
        }

        private void OnButtonStopClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            string projectUri = ((KeyValuePair<string, SvnExplorerSelection>)e.Argument).Key;
            SvnExplorerSelection searchSvnExplorerSelection = ((KeyValuePair<string, SvnExplorerSelection>)e.Argument).Value;
            Result = new List<string>();
            RecursiveParseDependency(Result, projectUri, searchSvnExplorerSelection.SvnUri, searchSvnExplorerSelection.RepositoryUri);
            e.Result = Result;
        }

        private void RecursiveParseDependency(IList<string> recursiveResult, string projectUri, string searchUri, string repository)
        {
            if (searchUri.Equals(projectUri, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            if (SvnHelper.IsUriTrunkTagBranch(searchUri))
            {
                string externalProperty = SvnHelper.GetProperty(searchUri, SvnHelper.EXTERNALS_PROPERTY_NAME);
                if (!string.IsNullOrEmpty(externalProperty))
                {
                    string[] externals = externalProperty.Split('\n');
                    foreach (string external in externals)
                    {
                        if (!string.IsNullOrEmpty(external))
                        {
                            string[] externalParts = external.Split(' ');
                            if (externalParts.Length > 0)
                            {
                                string realExternal = externalParts[0].Replace("%20", " ").Replace("^", repository);
                                if (realExternal.StartsWith(projectUri, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    if(!recursiveResult.Contains(searchUri))
                                        recursiveResult.Add(searchUri);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Collection<SvnInfoEventArgs> childrenInfo = SvnHelper.GetChildrenInfo(searchUri);
                foreach (SvnInfoEventArgs childInfo in childrenInfo)
                {
                    backgroundWorker.ReportProgress(0, childInfo.Uri.ToString());
                    RecursiveParseDependency(recursiveResult, projectUri, childInfo.Uri.ToString(), repository);
                }
            }
        }

        private void OnBackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelProgress.Text = string.Format(Resources.Scanning, e.UserState);
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Result = (List<string>) e.Result;
            if(e.Error == null)
            {
                DialogResult = DialogResult.OK;
            }
            Close();
        }
    }
}