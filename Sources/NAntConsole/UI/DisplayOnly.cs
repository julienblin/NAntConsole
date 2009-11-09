using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Entities;
using CDS.Framework.Tools.NAntConsole.Helpers;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class DisplayOnly : Form
    {
        private const string OPERATION_LOG = @"NAntConsole";
        private const int MAX_EVENT_LOG_MESSAGE_LENGTH = 31000;

        public DisplayOnly()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        readonly OutputBeautifier beautifier = new OutputBeautifier();

        private FileInfo deployFile;

        public FileInfo DeployFile
        {
            get { return deployFile; }
            set { deployFile = value; }
        }

        private string targetName = MainForm.INSTALL_TARGET_NAME;

        public string TargetName
        {
            get { return targetName; }
            set { targetName = value; }
        }

        private bool confirmInstallOrUninstall = true;

        public bool ConfirmInstallOrUninstall
        {
            get { return confirmInstallOrUninstall; }
            set { confirmInstallOrUninstall = value; }
        }


        private void ConnectEventHandlers()
        {
            backgroundWorker.DoWork += OnBackgroundWorkerNAntDoWork;
            backgroundWorker.ProgressChanged += OnBackgroundWorkerNAntProgressChanged;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerNAntRunWorkerCompleted;
            buttonOk.Click += OnButtonOkClick;
        }

        protected override void OnLoad(EventArgs e)
        {
            AssemblyName nantConsoleAssemblyName = Assembly.GetAssembly(typeof(MainForm)).GetName();
            Text = string.Format(Resources.MainFormTitle, nantConsoleAssemblyName.Version);

            ConfirmTarget();

            BackgroundWorkerArgument arg = new BackgroundWorkerArgument();
            arg.DeployFile = DeployFile;
            arg.TargetName = TargetName;
            backgroundWorker.RunWorkerAsync(arg);

            base.OnLoad(e);
        }

        private void OnBackgroundWorkerNAntDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorkerArgument arg = (BackgroundWorkerArgument)e.Argument;

            DirectoryInfo packageDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
            if (packageDir.Exists)
            {
                packageDir.Delete(true);
            }

            BackgroundWorkerResult result = new BackgroundWorkerResult();

            try
            {
                packageDir.Create();
                backgroundWorker.ReportProgress(0, string.Format(Resources.DisplayExecutionInfo, Environment.MachineName, Environment.UserDomainName, Environment.UserName, DateTime.Now));
                backgroundWorker.ReportProgress(0,
                                                string.Format(Resources.ExtractingPackage, arg.DeployFile.FullName,
                                                              packageDir.FullName));
                ZipHelper.CheckNAntConsoleVersion(DeployFile);
                ZipHelper.UnZip(DeployFile, packageDir);

                if(File.Exists(Path.Combine(packageDir.FullName, EnvIncludeConstants.VERSION_FILENAME)))
                {
                    try
                    {
                        result.PackageVersion = new Version(File.ReadAllText(Path.Combine(packageDir.FullName, EnvIncludeConstants.VERSION_FILENAME)));
                    }
                    catch
                    {
                    }
                }

                NAntProject nantProject = GetNantProject(packageDir);
                result.NAntProject = nantProject;
                int returnCode = NAntHelper.ExecuteNant(nantProject, arg.TargetName, delegate(NAntExecutionProgressEventArgs progressArgs)
                {
                    backgroundWorker.ReportProgress(0, progressArgs.Message);
                });
                result.OnError = (returnCode != 0);
                e.Result = result;
            }
            catch (VersionNotFoundException versionEx)
            {
                MessageBox.Show(this, versionEx.Message, Resources.ErrorCaption, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                backgroundWorker.ReportProgress(0, string.Format(Resources.ExecutionError, versionEx));
            }
            catch (Exception ex)
            {
                backgroundWorker.ReportProgress(0, string.Format(Resources.ExecutionError, ex));
            }
            finally
            {
                packageDir.Delete(true);
            }
        }

        private static NAntProject GetNantProject(DirectoryInfo packageDir)
        {
            FileInfo nantFile = new FileInfo(Path.Combine(packageDir.FullName, CompositeConstants.DEPLOY_FILE_NAME));
            return NAntHelper.LoadProject(nantFile);
        }

        private void OnBackgroundWorkerNAntProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            beautifier.Print((string)e.UserState, textBoxOutput);
            if (!ConfirmInstallOrUninstall)
            {
                Console.Out.WriteLine((string)e.UserState);
            }
        }

        private void OnBackgroundWorkerNAntRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
            WriteLog();
            if (e.Result != null)
            {
                WriteEventLog((BackgroundWorkerResult)e.Result);
            }

            if (ConfirmInstallOrUninstall)
            {
                buttonOk.Enabled = true;
                buttonOk.Focus();
            }
            else
            {
                Close();
            }
        }

        private void WriteLog()
        {
            string logFilePath = Path.Combine(DeployFile.DirectoryName, string.Format("{0}-{1}-{2}-{3:yyyyMMdd-HHmmss}.log", DeployFile.Name, Environment.MachineName, TargetName, DateTime.Now));
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath))
                {
                    foreach (string line in textBoxOutput.Lines)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format(Resources.DisplayOnlyErrorWritingLog, logFilePath, ex),
                                Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WriteEventLog(BackgroundWorkerResult result)
        {
            try
            {
                bool mustModifyOverflowSettings = false;
                if (!EventLog.Exists(OPERATION_LOG))
                {
                    mustModifyOverflowSettings = true;
                }

                string source = string.Concat(result.NAntProject.ProjectName, " - ", result.PackageVersion != null ? string.Concat(result.PackageVersion, " - ") : string.Empty, TargetName);
                if (EventLog.SourceExists(source))
                {
                    EventLog.DeleteEventSource(source);
                }
                EventLog.CreateEventSource(source, OPERATION_LOG);
                
                if (mustModifyOverflowSettings)
                {
                    EventLog log = new EventLog(OPERATION_LOG);
                    log.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 0);
                }
                
                string message = textBoxOutput.Text.Length > MAX_EVENT_LOG_MESSAGE_LENGTH
                                     ? textBoxOutput.Text.Substring(0, MAX_EVENT_LOG_MESSAGE_LENGTH)
                                     : textBoxOutput.Text;
                EventLog.WriteEntry(source, message, result.OnError ? EventLogEntryType.FailureAudit : EventLogEntryType.SuccessAudit, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format(Resources.DisplayOnlyErrorWritingEventLog, ex),
                                Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void OnButtonOkClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ConfirmTarget()
        {
            if (ConfirmInstallOrUninstall)
            {
                if (TargetName.Equals(MainForm.INSTALL_TARGET_NAME, StringComparison.InvariantCulture))
                {
                    if (MessageBox.Show(this, String.Format(Resources.ConfirmInstall, Path.GetFileNameWithoutExtension(DeployFile.FullName)), Resources.ConfirmationCaption, MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        Close();
                    }
                }

                if (TargetName.Equals(MainForm.UNINSTALL_TARGET_NAME, StringComparison.InvariantCulture))
                {
                    if (MessageBox.Show(this, String.Format(Resources.ConfirmUninstall, Path.GetFileNameWithoutExtension(DeployFile.FullName)), Resources.ConfirmationCaption, MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        Close();
                    }
                }
            }
        }

        class BackgroundWorkerArgument
        {
            public FileInfo DeployFile;
            public string TargetName;
        }

        class BackgroundWorkerResult
        {
            public NAntProject NAntProject;
            public bool OnError;
            public Version PackageVersion;
        }
    }
}