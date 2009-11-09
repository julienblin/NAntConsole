namespace CDS.Framework.Tools.NAntConsole.UI
{
    partial class MergeBranchExecute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeBranchExecute));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxMerge = new System.Windows.Forms.PictureBox();
            this.pictureBoxCheckinOutDest = new System.Windows.Forms.PictureBox();
            this.labelCheckingOutDest = new System.Windows.Forms.Label();
            this.labelMerge = new System.Windows.Forms.Label();
            this.pictureBoxUpdateProperties = new System.Windows.Forms.PictureBox();
            this.labelUpdateProperties = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.pictureBoxCommit = new System.Windows.Forms.PictureBox();
            this.labelCommit = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMerge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckinOutDest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpdateProperties)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCommit)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxMerge, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxCheckinOutDest, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelCheckingOutDest, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelMerge, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxUpdateProperties, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelUpdateProperties, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOK, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxCommit, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelCommit, 1, 3);
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
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // pictureBoxMerge
            // 
            this.pictureBoxMerge.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxMerge.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxMerge.Image")));
            this.pictureBoxMerge.Location = new System.Drawing.Point(3, 62);
            this.pictureBoxMerge.Name = "pictureBoxMerge";
            this.pictureBoxMerge.Size = new System.Drawing.Size(14, 16);
            this.pictureBoxMerge.TabIndex = 2;
            this.pictureBoxMerge.TabStop = false;
            // 
            // pictureBoxCheckinOutDest
            // 
            this.pictureBoxCheckinOutDest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxCheckinOutDest.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxCheckinOutDest.Image")));
            this.pictureBoxCheckinOutDest.Location = new System.Drawing.Point(3, 15);
            this.pictureBoxCheckinOutDest.Name = "pictureBoxCheckinOutDest";
            this.pictureBoxCheckinOutDest.Size = new System.Drawing.Size(14, 16);
            this.pictureBoxCheckinOutDest.TabIndex = 0;
            this.pictureBoxCheckinOutDest.TabStop = false;
            // 
            // labelCheckingOutDest
            // 
            this.labelCheckingOutDest.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCheckingOutDest.AutoSize = true;
            this.labelCheckingOutDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCheckingOutDest.Location = new System.Drawing.Point(23, 17);
            this.labelCheckingOutDest.Name = "labelCheckingOutDest";
            this.labelCheckingOutDest.Size = new System.Drawing.Size(207, 13);
            this.labelCheckingOutDest.TabIndex = 1;
            this.labelCheckingOutDest.Text = "Checking out destination uri ({0})...";
            // 
            // labelMerge
            // 
            this.labelMerge.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMerge.AutoSize = true;
            this.labelMerge.Location = new System.Drawing.Point(23, 64);
            this.labelMerge.Name = "labelMerge";
            this.labelMerge.Size = new System.Drawing.Size(162, 13);
            this.labelMerge.TabIndex = 1;
            this.labelMerge.Text = "Merging from {0} at revision {1}...";
            // 
            // pictureBoxUpdateProperties
            // 
            this.pictureBoxUpdateProperties.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxUpdateProperties.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxUpdateProperties.Image")));
            this.pictureBoxUpdateProperties.Location = new System.Drawing.Point(3, 109);
            this.pictureBoxUpdateProperties.Name = "pictureBoxUpdateProperties";
            this.pictureBoxUpdateProperties.Size = new System.Drawing.Size(14, 16);
            this.pictureBoxUpdateProperties.TabIndex = 2;
            this.pictureBoxUpdateProperties.TabStop = false;
            // 
            // labelUpdateProperties
            // 
            this.labelUpdateProperties.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelUpdateProperties.AutoSize = true;
            this.labelUpdateProperties.Location = new System.Drawing.Point(23, 111);
            this.labelUpdateProperties.Name = "labelUpdateProperties";
            this.labelUpdateProperties.Size = new System.Drawing.Size(108, 13);
            this.labelUpdateProperties.TabIndex = 1;
            this.labelUpdateProperties.Text = "Updating properties...";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelMain.SetColumnSpan(this.buttonOK, 2);
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(337, 192);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
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
            this.statusStrip.TabIndex = 2;
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
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Accept");
            this.imageList.Images.SetKeyName(1, "Cancel");
            // 
            // pictureBoxCommit
            // 
            this.pictureBoxCommit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxCommit.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxCommit.Image")));
            this.pictureBoxCommit.Location = new System.Drawing.Point(3, 156);
            this.pictureBoxCommit.Name = "pictureBoxCommit";
            this.pictureBoxCommit.Size = new System.Drawing.Size(14, 16);
            this.pictureBoxCommit.TabIndex = 2;
            this.pictureBoxCommit.TabStop = false;
            // 
            // labelCommit
            // 
            this.labelCommit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCommit.AutoSize = true;
            this.labelCommit.Location = new System.Drawing.Point(23, 158);
            this.labelCommit.Name = "labelCommit";
            this.labelCommit.Size = new System.Drawing.Size(81, 13);
            this.labelCommit.TabIndex = 1;
            this.labelCommit.Text = "Commiting {0}...";
            // 
            // MergeBranchExecute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 242);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MergeBranchExecute";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Merge branch";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMerge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckinOutDest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpdateProperties)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCommit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.PictureBox pictureBoxCheckinOutDest;
        private System.Windows.Forms.PictureBox pictureBoxMerge;
        private System.Windows.Forms.Label labelCheckingOutDest;
        private System.Windows.Forms.Label labelMerge;
        private System.Windows.Forms.PictureBox pictureBoxUpdateProperties;
        private System.Windows.Forms.Label labelUpdateProperties;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.PictureBox pictureBoxCommit;
        private System.Windows.Forms.Label labelCommit;
    }
}