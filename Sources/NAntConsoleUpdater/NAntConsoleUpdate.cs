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

namespace CDS.Framework.Tools.NAntConsoleUpdater
{
    public partial class NAntConsoleUpdate : Form
    {
        private const string NANTCONSOLE_EXE = @"CDS.Framework.Tools.NAntConsole.exe";
        private const string NANT_EXE = @"NAnt.exe";

        public NAntConsoleUpdate()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        private UpdateArgs updateArgs;

        public UpdateArgs UpdateArgs
        {
            get { return updateArgs; }
            set { updateArgs = value; }
        }

        private void ConnectEventHandlers()
        {
            backgroundWorker.DoWork += OnBackgroundWorkerDoWork;
            backgroundWorker.ProgressChanged += OnBackgroundWorkerProgressChanged;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            buttonOK.Click += OnButtonOKClick;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            KillBlockingProcesses();

            labelCopy.Text = string.Format(labelCopy.Text, UpdateArgs.UpdateFile.FullName);
            labelInstall.Text = string.Format(labelInstall.Text, UpdateArgs.TargetVersion);
            backgroundWorker.RunWorkerAsync(UpdateArgs);
        }

        private void KillBlockingProcesses()
        {
            Process[] runningNantConsoleProcesses = Process.GetProcessesByName(NANTCONSOLE_EXE);
            foreach (Process runningNantConsoleProcess in runningNantConsoleProcesses)
            {
                runningNantConsoleProcess.Kill();
            }

            Process[] runningNantProcesses = Process.GetProcessesByName(NANT_EXE);
            foreach (Process runningNantProcess in runningNantProcesses)
            {
                runningNantProcess.Kill();
            }
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            UpdateArgs args = (UpdateArgs)e.Argument;
            FileInfo localUpdateFile = null;
            try
            {
                localUpdateFile = args.UpdateFile.CopyTo(Path.Combine(Path.GetTempPath(), args.UpdateFile.Name), true);
                backgroundWorker.ReportProgress(1);

                KeyValuePair<int, string> returnInfo = ExecuteMsiExec(string.Format("/uninstall {{{0}}} /qn", args.ProductCode));
                if (returnInfo.Key != 0)
                {
                    backgroundWorker.ReportProgress(5);
                    MessageBox.Show(returnInfo.Value, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                backgroundWorker.ReportProgress(2);

                returnInfo = ExecuteMsiExec(string.Format("/package \"{0}\" /qn TARGETDIR=\"{1}\"", localUpdateFile.FullName, args.TargetDir.FullName));
                if (returnInfo.Key != 0)
                {
                    backgroundWorker.ReportProgress(6);
                    MessageBox.Show(returnInfo.Value, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                backgroundWorker.ReportProgress(3);

                localUpdateFile.Delete();
                backgroundWorker.ReportProgress(4);
            }
            finally
            {
                if ((localUpdateFile != null) && (localUpdateFile.Exists))
                {
                    try
                    {
                        localUpdateFile.Delete();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private static KeyValuePair<int, string> ExecuteMsiExec(string args)
        {
            string msiexec = string.Format("{0}\\msiexec.exe", Environment.SystemDirectory);
            StringBuilder output = new StringBuilder();
            ProcessStartInfo uninstallStartInfo = new ProcessStartInfo(msiexec);
            uninstallStartInfo.Arguments = args;
            Process process = Process.Start(uninstallStartInfo);
            process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs dataReceivedArgs)
                                              {
                                                  output.AppendLine(dataReceivedArgs.Data);
                                              };
            process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs dataReceivedArgs)
                                              {
                                                  output.AppendLine(dataReceivedArgs.Data);
                                              };
            process.WaitForExit();
            return new KeyValuePair<int, string>(process.ExitCode, output.ToString());
        }

        void uninstallProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            MessageBox.Show(e.Data);
        }

        private void OnBackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    labelCopy.Font = labelDeletePackage.Font;
                    pictureBoxCopy.Image = imageList.Images[0];
                    pictureBoxCopy.Size = imageList.Images[0].Size;
                    labelUninstall.Font = new Font(labelUninstall.Font, FontStyle.Bold);
                    break;
                case 2:
                    labelUninstall.Font = labelCopy.Font;
                    pictureBoxUninstall.Image = imageList.Images[0];
                    pictureBoxUninstall.Size = imageList.Images[0].Size;
                    labelInstall.Font = new Font(labelInstall.Font, FontStyle.Bold);
                    break;
                case 3:
                    labelInstall.Font = labelCopy.Font;
                    pictureBoxInstall.Image = imageList.Images[0];
                    pictureBoxInstall.Size = imageList.Images[0].Size;
                    labelDeletePackage.Font = new Font(labelDeletePackage.Font, FontStyle.Bold);
                    break;
                case 4:
                    labelDeletePackage.Font = labelCopy.Font;
                    pictureBoxDeletePackage.Image = imageList.Images[0];
                    pictureBoxDeletePackage.Size = imageList.Images[0].Size;
                    break;
                case 5:
                    pictureBoxUninstall.Image = imageList.Images[1];
                    pictureBoxUninstall.Size = imageList.Images[1].Size;
                    break;
                case 6:
                    pictureBoxInstall.Image = imageList.Images[1];
                    pictureBoxInstall.Size = imageList.Images[1].Size;
                    break;
            }
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, e.Error.Message, Resources.ErrorCaption, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                Close();
            }

            buttonOK.Enabled = true;
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
        }

        private void OnButtonOKClick(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(UpdateArgs.TargetDir.FullName, NANTCONSOLE_EXE));
            Close();
        }
    }
}