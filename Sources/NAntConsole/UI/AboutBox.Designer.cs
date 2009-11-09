namespace CDS.Framework.Tools.NAntConsole.UI
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.linkLabelNAntHelp = new System.Windows.Forms.LinkLabel();
            this.linkLabelReleaseNotes = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.okButton, 0, 6);
            this.tableLayoutPanelMain.Controls.Add(this.labelCopyright, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelVersion, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelProductName, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxLogo, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.linkLabelNAntHelp, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.linkLabelReleaseNotes, 0, 5);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 7;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(274, 290);
            this.tableLayoutPanelMain.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.okButton.AutoSize = true;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(196, 263);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 25;
            this.okButton.Text = "&OK";
            // 
            // labelCopyright
            // 
            this.labelCopyright.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(6, 185);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(51, 13);
            this.labelCopyright.TabIndex = 22;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(6, 158);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(42, 13);
            this.labelVersion.TabIndex = 21;
            this.labelVersion.Text = "Version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelProductName
            // 
            this.labelProductName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelProductName.AutoSize = true;
            this.labelProductName.Location = new System.Drawing.Point(6, 131);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(75, 13);
            this.labelProductName.TabIndex = 20;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.InitialImage")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(268, 118);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxLogo.TabIndex = 26;
            this.pictureBoxLogo.TabStop = false;
            // 
            // linkLabelNAntHelp
            // 
            this.linkLabelNAntHelp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabelNAntHelp.AutoSize = true;
            this.linkLabelNAntHelp.Location = new System.Drawing.Point(3, 212);
            this.linkLabelNAntHelp.Name = "linkLabelNAntHelp";
            this.linkLabelNAntHelp.Size = new System.Drawing.Size(102, 13);
            this.linkLabelNAntHelp.TabIndex = 27;
            this.linkLabelNAntHelp.TabStop = true;
            this.linkLabelNAntHelp.Text = "NAnt help reference";
            this.linkLabelNAntHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClicked);
            // 
            // linkLabelReleaseNotes
            // 
            this.linkLabelReleaseNotes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkLabelReleaseNotes.AutoSize = true;
            this.linkLabelReleaseNotes.Location = new System.Drawing.Point(3, 239);
            this.linkLabelReleaseNotes.Name = "linkLabelReleaseNotes";
            this.linkLabelReleaseNotes.Size = new System.Drawing.Size(75, 13);
            this.linkLabelReleaseNotes.TabIndex = 28;
            this.linkLabelReleaseNotes.TabStop = true;
            this.linkLabelReleaseNotes.Text = "Release notes";
            this.linkLabelReleaseNotes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkLabelReleaseNotesClicked);
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 308);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutBox";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.LinkLabel linkLabelNAntHelp;
        private System.Windows.Forms.LinkLabel linkLabelReleaseNotes;
    }
}
