namespace CDS.Framework.Tools.NAntConsole.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripActions = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNewProject = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemNewNantFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNewEnvironmentConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNewVbProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNewNetProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNewEmptyProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonOpenProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCheckOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorSvn = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowLinks = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddLink = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLinkAnalysis = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCreateBranch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMerge = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExtractCOMConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExtractIISConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCheckForUpdates = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxOutput = new System.Windows.Forms.RichTextBox();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.labelFileLocation = new System.Windows.Forms.Label();
            this.flowLayoutPanelTargets = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonOpenContainingFolder = new System.Windows.Forms.Button();
            this.buttonOpenInVS = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorkerNAnt = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerUICommand = new System.ComponentModel.BackgroundWorker();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.saveNAntFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorkerCheckSvnUpdates = new System.ComponentModel.BackgroundWorker();
            this.timerCheckSvnUpdates = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.toolStripActions.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 497);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(964, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripActions
            // 
            this.toolStripActions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNewProject,
            this.toolStripButtonOpenProject,
            this.toolStripButtonCheckOut,
            this.toolStripSeparatorSvn,
            this.toolStripButtonUpdate,
            this.toolStripButtonShowLinks,
            this.toolStripButtonAddLink,
            this.toolStripButtonLinkAnalysis,
            this.toolStripSeparator4,
            this.toolStripButtonCreateBranch,
            this.toolStripButtonMerge,
            this.toolStripSeparator1,
            this.toolStripButtonExtractCOMConfig,
            this.toolStripButtonExtractIISConfig,
            this.toolStripSeparator2,
            this.toolStripButtonSettings,
            this.toolStripButtonCheckForUpdates,
            this.toolStripButtonAbout});
            this.toolStripActions.Location = new System.Drawing.Point(0, 0);
            this.toolStripActions.Name = "toolStripActions";
            this.toolStripActions.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripActions.Size = new System.Drawing.Size(964, 25);
            this.toolStripActions.TabIndex = 1;
            this.toolStripActions.Text = "toolStrip1";
            // 
            // toolStripButtonNewProject
            // 
            this.toolStripButtonNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNewNantFile,
            this.toolStripMenuItemNewEnvironmentConfig,
            this.toolStripMenuItemNewVbProject,
            this.toolStripMenuItemNewNetProject,
            this.toolStripMenuItemNewEmptyProject});
            this.toolStripButtonNewProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNewProject.Image")));
            this.toolStripButtonNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewProject.Name = "toolStripButtonNewProject";
            this.toolStripButtonNewProject.Size = new System.Drawing.Size(32, 22);
            this.toolStripButtonNewProject.Text = "New project...";
            // 
            // toolStripMenuItemNewNantFile
            // 
            this.toolStripMenuItemNewNantFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemNewNantFile.Image")));
            this.toolStripMenuItemNewNantFile.Name = "toolStripMenuItemNewNantFile";
            this.toolStripMenuItemNewNantFile.Size = new System.Drawing.Size(213, 22);
            this.toolStripMenuItemNewNantFile.Text = "New .nant file...";
            // 
            // toolStripMenuItemNewEnvironmentConfig
            // 
            this.toolStripMenuItemNewEnvironmentConfig.Enabled = false;
            this.toolStripMenuItemNewEnvironmentConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemNewEnvironmentConfig.Image")));
            this.toolStripMenuItemNewEnvironmentConfig.Name = "toolStripMenuItemNewEnvironmentConfig";
            this.toolStripMenuItemNewEnvironmentConfig.Size = new System.Drawing.Size(213, 22);
            this.toolStripMenuItemNewEnvironmentConfig.Text = "New environment config...";
            // 
            // toolStripMenuItemNewVbProject
            // 
            this.toolStripMenuItemNewVbProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemNewVbProject.Image")));
            this.toolStripMenuItemNewVbProject.Name = "toolStripMenuItemNewVbProject";
            this.toolStripMenuItemNewVbProject.Size = new System.Drawing.Size(213, 22);
            this.toolStripMenuItemNewVbProject.Text = "New VB6 project...";
            // 
            // toolStripMenuItemNewNetProject
            // 
            this.toolStripMenuItemNewNetProject.Enabled = false;
            this.toolStripMenuItemNewNetProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemNewNetProject.Image")));
            this.toolStripMenuItemNewNetProject.Name = "toolStripMenuItemNewNetProject";
            this.toolStripMenuItemNewNetProject.Size = new System.Drawing.Size(213, 22);
            this.toolStripMenuItemNewNetProject.Text = "New .Net project...";
            // 
            // toolStripMenuItemNewEmptyProject
            // 
            this.toolStripMenuItemNewEmptyProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemNewEmptyProject.Image")));
            this.toolStripMenuItemNewEmptyProject.Name = "toolStripMenuItemNewEmptyProject";
            this.toolStripMenuItemNewEmptyProject.Size = new System.Drawing.Size(213, 22);
            this.toolStripMenuItemNewEmptyProject.Text = "New empty project...";
            // 
            // toolStripButtonOpenProject
            // 
            this.toolStripButtonOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenProject.Image")));
            this.toolStripButtonOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenProject.Name = "toolStripButtonOpenProject";
            this.toolStripButtonOpenProject.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpenProject.Text = "Open project...";
            // 
            // toolStripButtonCheckOut
            // 
            this.toolStripButtonCheckOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCheckOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCheckOut.Image")));
            this.toolStripButtonCheckOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCheckOut.Name = "toolStripButtonCheckOut";
            this.toolStripButtonCheckOut.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCheckOut.Text = "Checkout...";
            // 
            // toolStripSeparatorSvn
            // 
            this.toolStripSeparatorSvn.Name = "toolStripSeparatorSvn";
            this.toolStripSeparatorSvn.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonUpdate
            // 
            this.toolStripButtonUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUpdate.Enabled = false;
            this.toolStripButtonUpdate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUpdate.Image")));
            this.toolStripButtonUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUpdate.Name = "toolStripButtonUpdate";
            this.toolStripButtonUpdate.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUpdate.Text = "Update...";
            // 
            // toolStripButtonShowLinks
            // 
            this.toolStripButtonShowLinks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowLinks.Enabled = false;
            this.toolStripButtonShowLinks.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowLinks.Image")));
            this.toolStripButtonShowLinks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowLinks.Name = "toolStripButtonShowLinks";
            this.toolStripButtonShowLinks.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowLinks.Text = "Show links...";
            // 
            // toolStripButtonAddLink
            // 
            this.toolStripButtonAddLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddLink.Enabled = false;
            this.toolStripButtonAddLink.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddLink.Image")));
            this.toolStripButtonAddLink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddLink.Name = "toolStripButtonAddLink";
            this.toolStripButtonAddLink.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddLink.Text = "Add link to another project...";
            // 
            // toolStripButtonLinkAnalysis
            // 
            this.toolStripButtonLinkAnalysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLinkAnalysis.Enabled = false;
            this.toolStripButtonLinkAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLinkAnalysis.Image")));
            this.toolStripButtonLinkAnalysis.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLinkAnalysis.Name = "toolStripButtonLinkAnalysis";
            this.toolStripButtonLinkAnalysis.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLinkAnalysis.Text = "Search repository for other projects linked to the current...";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonCreateBranch
            // 
            this.toolStripButtonCreateBranch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCreateBranch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCreateBranch.Image")));
            this.toolStripButtonCreateBranch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreateBranch.Name = "toolStripButtonCreateBranch";
            this.toolStripButtonCreateBranch.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCreateBranch.Text = "Create branch...";
            // 
            // toolStripButtonMerge
            // 
            this.toolStripButtonMerge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMerge.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMerge.Image")));
            this.toolStripButtonMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMerge.Name = "toolStripButtonMerge";
            this.toolStripButtonMerge.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonMerge.Text = "Merge branch...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonExtractCOMConfig
            // 
            this.toolStripButtonExtractCOMConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExtractCOMConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExtractCOMConfig.Image")));
            this.toolStripButtonExtractCOMConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExtractCOMConfig.Name = "toolStripButtonExtractCOMConfig";
            this.toolStripButtonExtractCOMConfig.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExtractCOMConfig.Text = "Extract COM+ Config...";
            // 
            // toolStripButtonExtractIISConfig
            // 
            this.toolStripButtonExtractIISConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExtractIISConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExtractIISConfig.Image")));
            this.toolStripButtonExtractIISConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExtractIISConfig.Name = "toolStripButtonExtractIISConfig";
            this.toolStripButtonExtractIISConfig.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExtractIISConfig.Text = "Extract IIS Config...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSettings.Image")));
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSettings.Text = "Change settings...";
            // 
            // toolStripButtonCheckForUpdates
            // 
            this.toolStripButtonCheckForUpdates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCheckForUpdates.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCheckForUpdates.Image")));
            this.toolStripButtonCheckForUpdates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCheckForUpdates.Name = "toolStripButtonCheckForUpdates";
            this.toolStripButtonCheckForUpdates.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCheckForUpdates.Text = "Check for updates...";
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAbout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbout.Image")));
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAbout.Text = "About...";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 5;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.textBoxOutput, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelProjectName, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelFileLocation, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.flowLayoutPanelTargets, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOpenContainingFolder, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOpenInVS, 4, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonRefresh, 3, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(964, 472);
            this.tableLayoutPanelMain.TabIndex = 3;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxOutput, 4);
            this.textBoxOutput.DetectUrls = false;
            this.textBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOutput.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutput.ForeColor = System.Drawing.Color.White;
            this.textBoxOutput.Location = new System.Drawing.Point(158, 26);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.Size = new System.Drawing.Size(803, 423);
            this.textBoxOutput.TabIndex = 1;
            this.textBoxOutput.Text = "";
            this.textBoxOutput.WordWrap = false;
            // 
            // labelProjectName
            // 
            this.labelProjectName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProjectName.Location = new System.Drawing.Point(3, 1);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(149, 20);
            this.labelProjectName.TabIndex = 2;
            this.labelProjectName.Text = "labelProjectName";
            // 
            // labelFileLocation
            // 
            this.labelFileLocation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFileLocation.AutoSize = true;
            this.labelFileLocation.Location = new System.Drawing.Point(158, 5);
            this.labelFileLocation.Name = "labelFileLocation";
            this.labelFileLocation.Size = new System.Drawing.Size(86, 13);
            this.labelFileLocation.TabIndex = 3;
            this.labelFileLocation.Text = "labelFileLocation";
            // 
            // flowLayoutPanelTargets
            // 
            this.flowLayoutPanelTargets.AutoSize = true;
            this.flowLayoutPanelTargets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTargets.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelTargets.Location = new System.Drawing.Point(3, 26);
            this.flowLayoutPanelTargets.Name = "flowLayoutPanelTargets";
            this.flowLayoutPanelTargets.Size = new System.Drawing.Size(149, 423);
            this.flowLayoutPanelTargets.TabIndex = 4;
            // 
            // buttonOpenContainingFolder
            // 
            this.buttonOpenContainingFolder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonOpenContainingFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenContainingFolder.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenContainingFolder.Image")));
            this.buttonOpenContainingFolder.Location = new System.Drawing.Point(250, 3);
            this.buttonOpenContainingFolder.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.buttonOpenContainingFolder.Name = "buttonOpenContainingFolder";
            this.buttonOpenContainingFolder.Size = new System.Drawing.Size(23, 17);
            this.buttonOpenContainingFolder.TabIndex = 5;
            this.buttonOpenContainingFolder.TabStop = false;
            this.buttonOpenContainingFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonOpenContainingFolder.UseVisualStyleBackColor = true;
            this.buttonOpenContainingFolder.Visible = false;
            // 
            // buttonOpenInVS
            // 
            this.buttonOpenInVS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonOpenInVS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenInVS.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenInVS.Image")));
            this.buttonOpenInVS.Location = new System.Drawing.Point(302, 3);
            this.buttonOpenInVS.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.buttonOpenInVS.Name = "buttonOpenInVS";
            this.buttonOpenInVS.Size = new System.Drawing.Size(23, 17);
            this.buttonOpenInVS.TabIndex = 5;
            this.buttonOpenInVS.TabStop = false;
            this.buttonOpenInVS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonOpenInVS.UseVisualStyleBackColor = true;
            this.buttonOpenInVS.Visible = false;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.Location = new System.Drawing.Point(276, 3);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(23, 17);
            this.buttonRefresh.TabIndex = 6;
            this.buttonRefresh.TabStop = false;
            this.buttonRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Visible = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "NAnt files|*.nant";
            // 
            // backgroundWorkerNAnt
            // 
            this.backgroundWorkerNAnt.WorkerReportsProgress = true;
            // 
            // backgroundWorkerUICommand
            // 
            this.backgroundWorkerUICommand.WorkerReportsProgress = true;
            // 
            // saveNAntFileDialog
            // 
            this.saveNAntFileDialog.Filter = "NAnt file|*.nant";
            this.saveNAntFileDialog.Title = "Save .nant file";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 519);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.toolStripActions);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "NAntConsole";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStripActions.ResumeLayout(false);
            this.toolStripActions.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStrip toolStripActions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenProject;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.RichTextBox textBoxOutput;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.Label labelFileLocation;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTargets;
        private System.ComponentModel.BackgroundWorker backgroundWorkerNAnt;
        private System.Windows.Forms.ToolStripButton toolStripButtonCheckOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSvn;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreateBranch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.ToolStripSplitButton toolStripButtonNewProject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewVbProject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewNetProject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewEmptyProject;
        private System.ComponentModel.BackgroundWorker backgroundWorkerUICommand;
        private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
        private System.Windows.Forms.ToolStripButton toolStripButtonExtractIISConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonMerge;
        private System.Windows.Forms.ToolStripButton toolStripButtonExtractCOMConfig;
        private System.Windows.Forms.Button buttonOpenInVS;
        private System.Windows.Forms.Button buttonOpenContainingFolder;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewNantFile;
        private System.Windows.Forms.SaveFileDialog saveNAntFileDialog;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewEnvironmentConfig;
        private System.Windows.Forms.ToolStripButton toolStripButtonCheckForUpdates;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddLink;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowLinks;
        private System.Windows.Forms.ToolStripButton toolStripButtonLinkAnalysis;
        private System.Windows.Forms.ToolStripButton toolStripButtonUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.ComponentModel.BackgroundWorker backgroundWorkerCheckSvnUpdates;
        private System.Windows.Forms.Timer timerCheckSvnUpdates;
    }
}