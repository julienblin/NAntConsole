using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Configuration;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class MergeBranchExecute : Form
    {
        public MergeBranchExecute()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        private void ConnectEventHandlers()
        {
            backgroundWorker.DoWork += OnBackgroundWorkerDoWork;
            backgroundWorker.ProgressChanged += OnBackgroundWorkerProgressChanged;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            buttonOK.Click += OnButtonOKClick;
        }

        private SvnExplorerSelection source;

        public SvnExplorerSelection Source
        {
            get { return source; }
            set { source = value; }
        }

        private MergeBranchInfo mergeBranchInfo;

        public MergeBranchInfo MergeBranchInfo
        {
            get { return mergeBranchInfo; }
            set { mergeBranchInfo = value; }
        }

        private MergeBranchWizard.MergeChoice mergeChoice;
        private string destinationUri;
        private string sourceUri;
        private string checkOutPath;

        public MergeBranchWizard.MergeChoice MergeChoice
        {
            get { return mergeChoice; }
            set { mergeChoice = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            destinationUri = MergeChoice == MergeBranchWizard.MergeChoice.Reintegrate
                                 ? MergeBranchInfo.FromUri
                                 : Source.SvnUri;
            sourceUri = MergeChoice == MergeBranchWizard.MergeChoice.Reintegrate
                            ? Source.SvnUri
                            : MergeBranchInfo.FromUri;
            labelCheckingOutDest.Text = string.Format(labelCheckingOutDest.Text, destinationUri);
            labelMerge.Text = string.Format(labelMerge.Text, sourceUri, MergeBranchInfo.Revision);
            labelCommit.Text = string.Format(labelCommit.Text, destinationUri);
            backgroundWorker.RunWorkerAsync();
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            checkOutPath = Path.Combine(NAntConsoleConfigurationSection.GetCheckOutDirectory(),
                                         SvnHelper.GetPathFromSvnUri(destinationUri.Replace(Source.RepositoryUri, string.Empty)));
            if (Directory.Exists(checkOutPath))
            {
                if (SvnHelper.HasPendingModifications(checkOutPath))
                {
                    throw new ApplicationException(string.Format(Resources.HasPendingModifications, checkOutPath));
                }
            }

            SvnHelper.CheckOut(destinationUri, checkOutPath, null);
            backgroundWorker.ReportProgress(1);
            bool conflicts = SvnHelper.Merge(checkOutPath, sourceUri, MergeBranchInfo.Revision);
            backgroundWorker.ReportProgress(2);
            switch (MergeChoice)
            {
                case MergeBranchWizard.MergeChoice.Reintegrate:
                    SvnHelper.DeleteProperty(checkOutPath, SvnHelper.BRANCHED_AT_PROPERTY_NAME);
                    SvnHelper.DeleteProperty(checkOutPath, SvnHelper.BRANCHED_FROM_PROPERTY_NAME);
                    break;
                case MergeBranchWizard.MergeChoice.Update:
                    SvnHelper.SetProperty(checkOutPath, SvnHelper.BRANCHED_AT_PROPERTY_NAME, (MergeBranchInfo.Revision + 1).ToString());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            backgroundWorker.ReportProgress(3);
            if (!conflicts)
            {
                string message = string.Format(Resources.SvnLogCommitAfterMerge, sourceUri, MergeBranchInfo.Revision);
                SvnHelper.Commit(checkOutPath, message);
                backgroundWorker.ReportProgress(4);
            } else
            {
                backgroundWorker.ReportProgress(5);
                throw new ApplicationException(Resources.ResolveConflicts);
            }
        }

        private void OnBackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    labelCheckingOutDest.Font = labelMerge.Font;
                    pictureBoxCheckinOutDest.Image = imageList.Images[0];
                    pictureBoxCheckinOutDest.Size = imageList.Images[0].Size;
                    labelMerge.Font = new Font(labelMerge.Font, FontStyle.Bold);
                    break;
                case 2:
                    labelMerge.Font = labelCheckingOutDest.Font;
                    pictureBoxMerge.Image = imageList.Images[0];
                    pictureBoxMerge.Size = imageList.Images[0].Size;
                    labelUpdateProperties.Font = new Font(labelUpdateProperties.Font, FontStyle.Bold);
                    break;
                case 3:
                    labelUpdateProperties.Font = labelCheckingOutDest.Font;
                    pictureBoxUpdateProperties.Image = imageList.Images[0];
                    pictureBoxUpdateProperties.Size = imageList.Images[0].Size;
                    labelCommit.Font = new Font(labelCommit.Font, FontStyle.Bold);
                    break;
                case 4:
                    labelCommit.Font = labelCheckingOutDest.Font;
                    pictureBoxCommit.Image = imageList.Images[0];
                    pictureBoxCommit.Size = imageList.Images[0].Size;
                    break;
                case 5:
                    labelCommit.Font = labelCheckingOutDest.Font;
                    pictureBoxCommit.Image = imageList.Images[1];
                    pictureBoxCommit.Size = imageList.Images[1].Size;
                    break;
            }
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, e.Error.Message, Resources.ErrorCaption, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                Process.Start(checkOutPath);
                Close();
            }

            buttonOK.Enabled = true;
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
        }

        private void OnButtonOKClick(object sender, EventArgs e)
        {
           Close();
        }
    }
}