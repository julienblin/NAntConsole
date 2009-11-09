namespace CDS.Framework.Tools.NAntConsoleUpdater
{
    partial class NAntConsoleUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NAntConsoleUpdate));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxUninstall = new System.Windows.Forms.PictureBox();
            this.pictureBoxCopy = new System.Windows.Forms.PictureBox();
            this.labelCopy = new System.Windows.Forms.Label();
            this.labelUninstall = new System.Windows.Forms.Label();
            this.pictureBoxInstall = new System.Windows.Forms.PictureBox();
            this.labelInstall = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.pictureBoxDeletePackage = new System.Windows.Forms.PictureBox();
            this.labelDeletePackage = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUninstall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInstall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeletePackage)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxUninstall, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxCopy, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelCopy, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelUninstall, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxInstall, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelInstall, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOK, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxDeletePackage, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelDeletePackage, 1, 3);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 5;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(749, 220);
            this.tableLayoutPanelMain.TabIndex = 1;
            // 
            // pictureBoxUninstall
            // 
            this.pictureBoxUninstall.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxUninstall.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxUninstall.Image")));
            this.pictureBoxUninstall.Location = new System.Drawing.Point(3, 62);
            this.pictureBoxUninstall.Name = "pictureBoxUninstall";
            this.pictureBoxUninstall.Size = new System.Drawing.Size(14, 16);
            this.pictureBoxUninstall.TabIndex = 2;
            this.pictureBoxUninstall.TabStop = false;
            // 
            // pictureBoxCopy
            // 
            this.pictureBoxCopy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxCopy.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxCopy.Image")));
            this.pictureBoxCopy.Location = new System.Drawing.Point(3, 15);
            this.pictureBoxCopy.Name = "pictureBoxCopy";
            this.pictureBoxCopy.Size = new System.Drawing.Size(14, 16);
            this.pictureBoxCopy.TabIndex = 0;
            this.pictureBoxCopy.TabStop = false;
            // 
            // labelCopy
            // 
            this.labelCopy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCopy.AutoSize = true;
            this.labelCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopy.Location = new System.Drawing.Point(23, 17);
            this.labelCopy.Name = "labelCopy";
            this.labelCopy.Size = new System.Drawing.Size(166, 13);
            this.labelCopy.TabIndex = 1;
            this.labelCopy.Text = "Copying package from {0}...";
            // 
            // labelUninstall
            // 
            this.labelUninstall.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelUninstall.AutoSize = true;
            this.labelUninstall.Location = new System.Drawing.Point(23, 64);
            this.labelUninstall.Name = "labelUninstall";
            this.labelUninstall.Size = new System.Drawing.Size(150, 13);
            this.labelUninstall.TabIndex = 1;
            this.labelUninstall.Text = "Uninstalling previous version...";
            // 
            // pictureBoxInstall
            // 
            this.pictureBoxInstall.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxInstall.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxInstall.Image")));
            this.pictureBoxInstall.Location = new System.Drawing.Point(3, 109);
            this.pictureBoxInstall.Name = "pictureBoxInstall";
            this.pictureBoxInstall.Size = new System.Drawing.Size(14, 16);
            this.pictureBoxInstall.TabIndex = 2;
            this.pictureBoxInstall.TabStop = false;
            // 
            // labelInstall
            // 
            this.labelInstall.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelInstall.AutoSize = true;
            this.labelInstall.Location = new System.Drawing.Point(23, 111);
            this.labelInstall.Name = "labelInstall";
            this.labelInstall.Size = new System.Drawing.Size(111, 13);
            this.labelInstall.TabIndex = 1;
            this.labelInstall.Text = "Installing version {0}...";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOK.AutoSize = true;
            this.tableLayoutPanelMain.SetColumnSpan(this.buttonOK, 2);
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(310, 192);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(128, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "Relaunch NAntConsole";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // pictureBoxDeletePackage
            // 
            this.pictureBoxDeletePackage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxDeletePackage.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxDeletePackage.Image")));
            this.pictureBoxDeletePackage.Location = new System.Drawing.Point(3, 156);
            this.pictureBoxDeletePackage.Name = "pictureBoxDeletePackage";
            this.pictureBoxDeletePackage.Size = new System.Drawing.Size(14, 16);
            this.pictureBoxDeletePackage.TabIndex = 2;
            this.pictureBoxDeletePackage.TabStop = false;
            // 
            // labelDeletePackage
            // 
            this.labelDeletePackage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDeletePackage.AutoSize = true;
            this.labelDeletePackage.Location = new System.Drawing.Point(23, 158);
            this.labelDeletePackage.Name = "labelDeletePackage";
            this.labelDeletePackage.Size = new System.Drawing.Size(131, 13);
            this.labelDeletePackage.TabIndex = 1;
            this.labelDeletePackage.Text = "Cleaning install package...";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 220);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(749, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 3;
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
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Accept");
            this.imageList.Images.SetKeyName(1, "Cancel");
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            // 
            // NAntConsoleUpdate
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 242);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NAntConsoleUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updating NAntConsole";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUninstall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInstall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeletePackage)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.PictureBox pictureBoxUninstall;
        private System.Windows.Forms.PictureBox pictureBoxCopy;
        private System.Windows.Forms.Label labelCopy;
        private System.Windows.Forms.Label labelUninstall;
        private System.Windows.Forms.PictureBox pictureBoxInstall;
        private System.Windows.Forms.Label labelInstall;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.PictureBox pictureBoxDeletePackage;
        private System.Windows.Forms.Label labelDeletePackage;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ImageList imageList;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}