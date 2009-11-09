namespace CDS.Framework.Tools.NAntConsole.UI
{
    partial class SvnExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SvnExplorer));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxRepository = new System.Windows.Forms.ComboBox();
            this.treeViewFolders = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCreateFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCreateBTT = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.comboBoxRepository, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.treeViewFolders, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOk, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.buttonCancel, 1, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(729, 487);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // comboBoxRepository
            // 
            this.comboBoxRepository.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelMain.SetColumnSpan(this.comboBoxRepository, 2);
            this.comboBoxRepository.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRepository.FormattingEnabled = true;
            this.comboBoxRepository.Location = new System.Drawing.Point(3, 3);
            this.comboBoxRepository.Name = "comboBoxRepository";
            this.comboBoxRepository.Size = new System.Drawing.Size(723, 21);
            this.comboBoxRepository.TabIndex = 0;
            // 
            // treeViewFolders
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.treeViewFolders, 2);
            this.treeViewFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFolders.ImageKey = "Folder";
            this.treeViewFolders.ImageList = this.imageList;
            this.treeViewFolders.Location = new System.Drawing.Point(3, 30);
            this.treeViewFolders.Name = "treeViewFolders";
            this.treeViewFolders.SelectedImageKey = "OpenFolder";
            this.treeViewFolders.Size = new System.Drawing.Size(723, 425);
            this.treeViewFolders.TabIndex = 1;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Folder");
            this.imageList.Images.SetKeyName(1, "OpenFolder");
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonOk.AutoSize = true;
            this.buttonOk.Location = new System.Drawing.Point(570, 461);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(651, 461);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 487);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(729, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
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
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCreateFolder,
            this.toolStripMenuItemCreateBTT});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(232, 48);
            // 
            // toolStripMenuItemCreateFolder
            // 
            this.toolStripMenuItemCreateFolder.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCreateFolder.Image")));
            this.toolStripMenuItemCreateFolder.Name = "toolStripMenuItemCreateFolder";
            this.toolStripMenuItemCreateFolder.Size = new System.Drawing.Size(231, 22);
            this.toolStripMenuItemCreateFolder.Text = "Create folder...";
            // 
            // toolStripMenuItemCreateBTT
            // 
            this.toolStripMenuItemCreateBTT.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCreateBTT.Image")));
            this.toolStripMenuItemCreateBTT.Name = "toolStripMenuItemCreateBTT";
            this.toolStripMenuItemCreateBTT.Size = new System.Drawing.Size(231, 22);
            this.toolStripMenuItemCreateBTT.Text = "Create branches, tags && trunk";
            // 
            // SvnExplorer
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(729, 509);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "SvnExplorer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SvnExplorer";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.ComboBox comboBoxRepository;
        private System.Windows.Forms.TreeView treeViewFolders;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCreateFolder;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCreateBTT;
    }
}