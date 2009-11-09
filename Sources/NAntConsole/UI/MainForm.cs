using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Activa.LazyParser;
using CDS.Framework.Tools.NAntConsole.Configuration;
using CDS.Framework.Tools.NAntConsole.Entities;
using CDS.Framework.Tools.NAntConsole.UI.Commands;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class MainForm : Form
    {
        public const string INSTALL_TARGET_NAME = @"install";
        public const string UNINSTALL_TARGET_NAME = @"uninstall";

        public MainForm()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        private FileInfo projectFile;

        public FileInfo ProjectFile
        {
            get { return projectFile; }
            set
            {
                projectFile = value;
                InvokeProjectFileChanged(new EventArgs());
            }
        }

        private NAntProject nantProject;
        readonly OutputBeautifier beautifier = new OutputBeautifier();
        readonly NAntDisplayTargetsFilter targetFilter = new NAntDisplayTargetsFilter();
        readonly VisualStudioIntegration vsIntegration = new VisualStudioIntegration();
        private string lastTargetNameExecuted;

        protected override void OnLoad(EventArgs e)
        {
            AssemblyName nantConsoleAssemblyName = Assembly.GetAssembly(typeof(MainForm)).GetName();
            Text = string.Format(Resources.MainFormTitle, nantConsoleAssemblyName.Version);

            toolStripStatusLabel.Text = Resources.ToolStripStatusLabel;
            if (ProjectFile == null)
                OnMainFormProjectFileChanged(this, new EventArgs());
            labelProjectName.Focus();

            toolTip.SetToolTip(buttonOpenInVS, Resources.OpenInVS);
            toolTip.SetToolTip(buttonRefresh, Resources.Refresh);
            toolTip.SetToolTip(buttonOpenContainingFolder, Resources.OpenContainingFolder);

            base.OnLoad(e);

            NAntConsoleConfigurationSection configurationSection =
                NAntConsoleConfigurationSection.GetConfigurationSection();
            timerCheckSvnUpdates.Interval = configurationSection.TimespanBetweenCheckForUpdatesInMinutes * 60 * 1000;
            timerCheckSvnUpdates.Start();

        }

        internal event EventHandler<EventArgs> ProjectFileChanged;

        private void ConnectEventHandlers()
        {
            toolStripButtonOpenProject.Click += OnToolStripButtonOpenProjectClick;
            toolStripButtonAbout.Click += OnToolStripButtonAboutClick;
            toolStripButtonCheckOut.Click += OnToolStripButtonCheckOutClick;
            toolStripButtonNewProject.Click += OnToolStripButtonNewProjectClick;
            toolStripButtonSettings.Click += OnToolStripButtonSettingsClick;
            toolStripButtonExtractIISConfig.Click += OnToolStripButtonExtractIISConfigClick;
            toolStripButtonExtractCOMConfig.Click += OnToolStripButtonExtractCOMConfigClick;
            toolStripButtonCreateBranch.Click += OnToolStripButtonCreateBranchClick;
            toolStripButtonMerge.Click += OnToolStripButtonMergeClick;
            toolStripButtonCheckForUpdates.Click += OnToolStripButtonCheckForUpdatesClick;
            toolStripButtonUpdate.Click += OnToolStripButtonUpdateClick;
            toolStripButtonAddLink.Click += OnToolStripButtonAddLinkClick;
            toolStripButtonShowLinks.Click += OnToolStripButtonShowLinksClick;
            toolStripButtonLinkAnalysis.Click += OnToolStripButtonLinkAnalysisClick;
            toolStripMenuItemNewNantFile.Click += OnToolStripMenuItemNewNantFileClick;
            toolStripMenuItemNewEnvironmentConfig.Click += OnToolStripMenuItemNewEnvironmentConfigClick;
            toolStripMenuItemNewVbProject.Click += OnToolStripMenuItemNewProjectClick;
            toolStripMenuItemNewNetProject.Click += OnToolStripMenuItemNewProjectClick;
            toolStripMenuItemNewEmptyProject.Click += OnToolStripMenuItemNewProjectClick;
            buttonOpenContainingFolder.Click += OnButtonOpenContainingFolderClick;
            buttonRefresh.Click += OnButtonRefreshClick;
            buttonOpenInVS.Click += OnButtonOpenInVSClick;
            backgroundWorkerNAnt.DoWork += OnBackgroundWorkerNAntDoWork;
            backgroundWorkerNAnt.ProgressChanged += OnBackgroundWorkerNAntProgressChanged;
            backgroundWorkerNAnt.RunWorkerCompleted += OnBackgroundWorkerNAntRunWorkerCompleted;
            backgroundWorkerUICommand.DoWork += OnBackgroundWorkerUICommandDoWork;
            backgroundWorkerUICommand.ProgressChanged += OnBackgroundWorkerUICommandProgressChanged;
            backgroundWorkerUICommand.RunWorkerCompleted += OnBackgroundWorkerUICommandRunWorkerCompleted;
            backgroundWorkerCheckSvnUpdates.DoWork += OnBackgroundWorkerCheckSvnUpdatesDoWork;
            backgroundWorkerCheckSvnUpdates.RunWorkerCompleted += OnBackgroundWorkerCheckSvnUpdatesRunWorkerCompleted;
            timerCheckSvnUpdates.Tick += OnTimerCheckSvnUpdatesTick;
            ProjectFileChanged += OnMainFormProjectFileChanged;
        }

        private void InvokeProjectFileChanged(EventArgs e)
        {
            EventHandler<EventArgs> projectFileChangedHandler = ProjectFileChanged;
            if (projectFileChangedHandler != null) projectFileChangedHandler(this, e);
        }

        void OnMainFormProjectFileChanged(object sender, EventArgs e)
        {
            LoadProject();
            if (nantProject == null)
            {
                labelProjectName.Text = string.Empty;
                labelFileLocation.Text = Resources.ToolStripLabelLocationOpenProjectFile;
                buttonOpenContainingFolder.Visible = false;
                buttonRefresh.Visible = false;
                buttonOpenInVS.Visible = false;
                toolStripMenuItemNewEnvironmentConfig.Enabled = false;
                toolStripButtonUpdate.Enabled = false;
                toolStripButtonAddLink.Enabled = false;
                toolStripButtonShowLinks.Enabled = false;
                toolStripButtonLinkAnalysis.Enabled = false;
            }
            else
            {
                labelProjectName.Text = nantProject.ProjectName;
                labelFileLocation.Text = nantProject.BuildFile.FullName;
                buttonOpenContainingFolder.Visible = true;
                buttonRefresh.Visible = true;
                toolStripMenuItemNewEnvironmentConfig.Enabled = true;
                toolStripButtonUpdate.Enabled = true;
                toolStripButtonAddLink.Enabled = true;
                toolStripButtonShowLinks.Enabled = true;
                toolStripButtonLinkAnalysis.Enabled = true;
                if (vsIntegration.IsVisualStudioInstalled())
                {
                    buttonOpenInVS.Visible = true;
                }
                backgroundWorkerCheckSvnUpdates.RunWorkerAsync();
            }
        }

        void OnBackgroundWorkerNAntDoWork(object sender, DoWorkEventArgs e)
        {
            string targetName = (string)e.Argument;
            int returnCode = NAntHelper.ExecuteNant(nantProject, targetName, delegate(NAntExecutionProgressEventArgs progressArgs)
            {
                backgroundWorkerNAnt.ReportProgress(0, progressArgs.Message);
            });
            e.Result = returnCode;
        }

        void OnBackgroundWorkerNAntProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            beautifier.Print((string)e.UserState, textBoxOutput);
        }

        void OnBackgroundWorkerNAntRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
            int returnCode = (int)e.Result;
            if (returnCode == 0)
            {
                toolStripStatusLabel.Text = string.Format(Resources.ToolStripStatusLabelExecutionOk, lastTargetNameExecuted);
            }
            else
            {
                toolStripStatusLabel.Text = string.Format(Resources.ToolStripStatusLabelExecutionKo, lastTargetNameExecuted);
            }
        }

        private void RunSynchronousCommand(IUICommand command)
        {
            RunSynchronousCommand(command, null);
        }

        private void RunSynchronousCommand(IUICommand command, RunSynchronousCommandFinished finished)
        {
            try
            {
                CommandExecutionResult result = command.Execute();
                if (result.Error == null)
                {
                    toolStripStatusLabel.Text = string.Format(Resources.ToolStripStatusLabelExecutionOk, result.Command.CommandName);
                    if (result.NewProjectFile != null)
                    {
                        ProjectFile = result.NewProjectFile;
                    }

                    if (!string.IsNullOrEmpty(result.Message))
                    {
                        textBoxOutput.AppendText(result.Message);
                    }
                }
                else
                {
                    textBoxOutput.Clear();
                    textBoxOutput.AppendText(result.Error.ToString());
                    toolStripStatusLabel.Text = string.Format(Resources.ToolStripStatusLabelExecutionKo, result.Command.CommandName);
                }

                ScrollHelper.ScrollToBottomLeft(textBoxOutput);

                if (finished != null)
                {
                    finished(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void RunASyncCommand(IUICommand command)
        {
            foreach (Control control in flowLayoutPanelTargets.Controls)
            {
                control.Enabled = false;
            }
            toolStripStatusLabel.Text = string.Format(Resources.ToolStripStatusLabelExecutingTarget, command.CommandName);
            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
            backgroundWorkerUICommand.RunWorkerAsync(command);
        }

        private delegate void RunSynchronousCommandFinished(CommandExecutionResult result);

        private void OnBackgroundWorkerUICommandDoWork(object sender, DoWorkEventArgs e)
        {
            IUICommand command = (IUICommand)e.Argument;
            IUICommandReportProgressEventArgs initReportProgress = new IUICommandReportProgressEventArgs(string.Format(Resources.InitUICommand, command.CommandName));
            backgroundWorkerUICommand.ReportProgress(0, initReportProgress);
            command.ReportProgress += delegate(object reportProgresssender, IUICommandReportProgressEventArgs eventArgs)
                                          {
                                              backgroundWorkerUICommand.ReportProgress(0, eventArgs);
                                          };
            e.Result = command.Execute();
        }

        private void OnBackgroundWorkerUICommandProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            IUICommandReportProgressEventArgs reportProgressArgs = (IUICommandReportProgressEventArgs)e.UserState;
            beautifier.Print(reportProgressArgs.Message, textBoxOutput);
            ScrollHelper.ScrollToBottomLeft(textBoxOutput);
        }

        private void OnBackgroundWorkerUICommandRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
            foreach (Control control in flowLayoutPanelTargets.Controls)
            {
                control.Enabled = true;
            }

            CommandExecutionResult result = (CommandExecutionResult)e.Result;
            if (result.Error == null)
            {
                toolStripStatusLabel.Text = string.Format(Resources.ToolStripStatusLabelExecutionOk, result.Command.CommandName);
                if (result.NewProjectFile != null)
                {
                    ProjectFile = result.NewProjectFile;
                }
            }
            else
            {
                textBoxOutput.Clear();
                textBoxOutput.AppendText(result.Error.ToString());
                toolStripStatusLabel.Text = string.Format(Resources.ToolStripStatusLabelExecutionKo, result.Command.CommandName);
            }
            ScrollHelper.ScrollToBottomLeft(textBoxOutput);
        }

        private void OnBackgroundWorkerCheckSvnUpdatesDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = false;
            try
            {
                if (nantProject != null)
                {
                    e.Result = SvnHelper.AreUpdatesAvalaible(nantProject.BuildFile.Directory.FullName);
                }
            }
            catch
            {
            }
        }

        private void OnBackgroundWorkerCheckSvnUpdatesRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result)
            {
                toolStripButtonUpdate.Image = Images.Images.SVNUpdateAvalaibles;
                toolStripButtonUpdate.ToolTipText = Resources.UpdateAvalaibleToolTip;
                if (toolStripButtonUpdate.Image != Images.Images.SVNUpdateAvalaibles)
                    toolTip.Show(Resources.UpdateAvalaibleToolTip, labelProjectName);
            }
            else
            {
                toolStripButtonUpdate.Image = Images.Images.SVNUpdate;
                toolStripButtonUpdate.ToolTipText = Resources.UpdateToolTip;
            }
        }

        private void OnTimerCheckSvnUpdatesTick(object sender, EventArgs e)
        {
            backgroundWorkerCheckSvnUpdates.RunWorkerAsync();
        }

        private void LoadProject()
        {
            nantProject = null;
            flowLayoutPanelTargets.Controls.Clear();
            if (projectFile == null) return;

            try
            {
                nantProject = NAntHelper.LoadProject(ProjectFile);

                if (nantProject != null)
                {
                    foreach (NAntTarget target in nantProject.Targets)
                    {
                        if (targetFilter.Display(target))
                        {
                            Button targetButton = new Button();
                            targetButton.AutoSize = true;
                            targetButton.Anchor = AnchorStyles.None;
                            targetButton.Text = target.Name;
                            targetButton.Click += OnTargetButtonClick;

                            toolTip.SetToolTip(targetButton, CreateToolTipFromTarget(target));

                            if (target.Name.Equals(nantProject.DefaultTargetName,
                                                   StringComparison.InvariantCultureIgnoreCase))
                            {
                                targetButton.Font = new Font(targetButton.Font, FontStyle.Bold);
                            }

                            flowLayoutPanelTargets.Controls.Add(targetButton);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format(Resources.ErrorLoadingProjectFile, ProjectFile.FullName, ex),
                                Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string CreateToolTipFromTarget(NAntTarget target)
        {
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrEmpty(target.Description))
            {
                result.AppendLine(target.Description);
            }
            else
            {
                result.AppendLine(target.Name);
            }

            CreateDependenciesToolTip(target, result, 1);

            return result.ToString();
        }

        private static void CreateDependenciesToolTip(NAntTarget target, StringBuilder result, int tabLevel)
        {
            string tabs = new string('\t', tabLevel);
            foreach (string dependencyName in target.Dependencies)
            {
                NAntTarget depTarget = target.Project.FindTargetByName(dependencyName);
                if (depTarget != null)
                {
                    result.AppendLine(string.Format("{0}|-> {1} ({2})", tabs, depTarget.Name, depTarget.Description));
                    CreateDependenciesToolTip(depTarget, result, tabLevel + 1);
                }
            }
        }

        void OnTargetButtonClick(object sender, EventArgs e)
        {
            Button targetButton = (Button)sender;

            if (targetButton.Text.Equals(INSTALL_TARGET_NAME, StringComparison.InvariantCulture))
            {
                if (MessageBox.Show(this, String.Format(Resources.ConfirmInstall, nantProject.ProjectName), Resources.ConfirmationCaption, MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }

            if (targetButton.Text.Equals(UNINSTALL_TARGET_NAME, StringComparison.InvariantCulture))
            {
                if (MessageBox.Show(this, String.Format(Resources.ConfirmUninstall, nantProject.ProjectName), Resources.ConfirmationCaption, MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }

            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
            toolStripStatusLabel.Text = string.Format(Resources.ToolStripStatusLabelExecutingTarget, targetButton.Text);
            textBoxOutput.Text = string.Empty;
            lastTargetNameExecuted = targetButton.Text;
            backgroundWorkerNAnt.RunWorkerAsync(targetButton.Text);
        }

        private void OnToolStripButtonOpenProjectClick(object sender, EventArgs e)
        {
            if (ProjectFile != null)
            {
                openFileDialog.FileName = ProjectFile.FullName;
            }
            else
            {
                openFileDialog.InitialDirectory = NAntConsoleConfigurationSection.GetCheckOutDirectory();
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ProjectFile = new FileInfo(openFileDialog.FileName);
            }
        }

        private void OnToolStripButtonAboutClick(object sender, EventArgs e)
        {
            (new AboutBox()).ShowDialog();
        }

        private void OnToolStripButtonCheckOutClick(object sender, EventArgs e)
        {
            // Access CheckOutdirectory to ensure there is a value.
            NAntConsoleConfigurationSection.GetCheckOutDirectory();
            SvnExplorer svnExplorer = new SvnExplorer();
            if (svnExplorer.ShowDialog(this) == DialogResult.OK)
            {
                CheckOutUICommand checkOutUICommand = new CheckOutUICommand(svnExplorer.GetSvnExplorerSelection());
                RunASyncCommand(checkOutUICommand);
            }
        }

        private void OnToolStripButtonNewProjectClick(object sender, EventArgs e)
        {
            toolStripButtonNewProject.ShowDropDown();
        }

        private void OnToolStripButtonSettingsClick(object sender, EventArgs e)
        {
            NAntConsoleConfigurationSection.AskCheckOutDirectory();
        }

        private void OnToolStripButtonExtractIISConfigClick(object sender, EventArgs e)
        {
            (new IISMetabaseExtractor()).ShowDialog();
        }

        private void OnToolStripButtonExtractCOMConfigClick(object sender, EventArgs e)
        {
            (new COMComponentsExtractor()).ShowDialog();
        }

        private void OnToolStripButtonCreateBranchClick(object sender, EventArgs e)
        {
            SvnExplorer svnExplorer = new SvnExplorer();
            svnExplorer.TrunkSubTagOrSubBranchOnly = true;
            if (svnExplorer.ShowDialog(this) == DialogResult.OK)
            {
                IUICommand command = new CreateBranchCommand(svnExplorer.GetSvnExplorerSelection());
                RunSynchronousCommand(command);
            }
        }

        private void OnToolStripButtonMergeClick(object sender, EventArgs e)
        {
            SvnExplorer svnExplorer = new SvnExplorer();
            svnExplorer.TrunkSubTagOrSubBranchOnly = true;
            if (svnExplorer.ShowDialog(this) == DialogResult.OK)
            {
                IUICommand command = new MergeBranchCommand(svnExplorer.GetSvnExplorerSelection());
                RunSynchronousCommand(command);
            }
        }

        private void OnToolStripMenuItemNewNantFileClick(object sender, EventArgs e)
        {
            if (saveNAntFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                IUICommand command = new NewNAntFileCommand(saveNAntFileDialog.FileName);
                RunSynchronousCommand(command);
            }
        }

        private void OnToolStripMenuItemNewEnvironmentConfigClick(object sender, EventArgs e)
        {
            IUICommand command = new NewEnvironmentConfigFileCommand(nantProject);
            RunSynchronousCommand(command);
        }

        private void OnToolStripButtonCheckForUpdatesClick(object sender, EventArgs e)
        {
            IUICommand command = new CheckForUpdatesCommand();
            RunSynchronousCommand(command);
        }

        private void OnToolStripButtonUpdateClick(object sender, EventArgs e)
        {
            IUICommand command = new UpdateCommand(nantProject);
            RunASyncCommand(command);
            toolStripButtonUpdate.Image = Images.Images.SVNUpdate;
            toolStripButtonUpdate.ToolTipText = Resources.UpdateToolTip;
        }

        private void OnToolStripButtonAddLinkClick(object sender, EventArgs e)
        {
            IUICommand command = new AddLinkCommand(this, nantProject);
            RunSynchronousCommand(command, delegate(CommandExecutionResult result)
                                               {
                                                   if ((bool)result.CommandOutput)
                                                   {
                                                       IUICommand updateCommand = new UpdateCommand(nantProject);
                                                       RunASyncCommand(updateCommand);
                                                   }
                                               });
        }

        private void OnToolStripButtonShowLinksClick(object sender, EventArgs e)
        {
            IUICommand command = new ShowLinksCommand(this, nantProject);
            RunSynchronousCommand(command);
        }

        private void OnToolStripButtonLinkAnalysisClick(object sender, EventArgs e)
        {
            IUICommand command = new LinksAnalysisCommand(this, nantProject);
            RunSynchronousCommand(command);
        }

        private void OnToolStripMenuItemNewProjectClick(object sender, EventArgs e)
        {
            // Access CheckOutdirectory to ensure there is a value.
            NAntConsoleConfigurationSection.GetCheckOutDirectory();
            SvnExplorer svnExplorer = new SvnExplorer();
            svnExplorer.ReadOnly = false;
            svnExplorer.TrunkSubTagOrSubBranchOnly = true;
            if (svnExplorer.ShowDialog(this) == DialogResult.OK)
            {
                IUICommand command = null;
                if (sender == toolStripMenuItemNewVbProject)
                {
                    command = new GenerateVB6ProjectCommand(svnExplorer.GetSvnExplorerSelection());
                }

                if (sender == toolStripMenuItemNewEmptyProject)
                {
                    command = new GenerateEmptyProjectCommand(svnExplorer.GetSvnExplorerSelection());
                }

                if (command != null)
                {
                    RunASyncCommand(command);
                }
            }
        }

        private void OnButtonOpenContainingFolderClick(object sender, EventArgs e)
        {
            Process.Start(nantProject.BuildFile.DirectoryName);
        }

        private void OnButtonRefreshClick(object sender, EventArgs e)
        {
            ProjectFile = ProjectFile;
        }

        private void OnButtonOpenInVSClick(object sender, EventArgs e)
        {
            try
            {
                vsIntegration.OpenFileInVS(nantProject.BuildFile.FullName);
            }
            catch
            {
                MessageBox.Show(this, Resources.OpenInVSError, Resources.ErrorCaption, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}