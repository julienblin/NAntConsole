namespace CDS.Framework.Tools.NAntConsole.UI
{
    partial class COMComponentsExtractor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(COMComponentsExtractor));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.labelServerName = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.treeViewComponents = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.textBoxCopyInformation = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.labelServerName, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxServerName, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonConnect, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.treeViewComponents, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxCopyInformation, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOK, 2, 3);
            this.tableLayoutPanelMain.Controls.Add(this.buttonCopyToClipboard, 1, 3);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 4;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(613, 587);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // labelServerName
            // 
            this.labelServerName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelServerName.AutoSize = true;
            this.labelServerName.Location = new System.Drawing.Point(3, 8);
            this.labelServerName.Name = "labelServerName";
            this.labelServerName.Size = new System.Drawing.Size(73, 13);
            this.labelServerName.TabIndex = 0;
            this.labelServerName.Text = "Server name :";
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServerName.Location = new System.Drawing.Point(82, 4);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(447, 20);
            this.textBoxServerName.TabIndex = 1;
            this.textBoxServerName.Text = "localhost";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonConnect.Location = new System.Drawing.Point(535, 3);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            // 
            // treeViewComponents
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.treeViewComponents, 3);
            this.treeViewComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewComponents.ImageIndex = 0;
            this.treeViewComponents.ImageList = this.imageList;
            this.treeViewComponents.Location = new System.Drawing.Point(3, 32);
            this.treeViewComponents.Name = "treeViewComponents";
            this.treeViewComponents.SelectedImageIndex = 0;
            this.treeViewComponents.Size = new System.Drawing.Size(607, 152);
            this.treeViewComponents.TabIndex = 3;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "ComponentServices");
            this.imageList.Images.SetKeyName(1, "COMLibrary");
            this.imageList.Images.SetKeyName(2, "COMServer");
            this.imageList.Images.SetKeyName(3, "COMComponent");
            // 
            // textBoxCopyInformation
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxCopyInformation, 3);
            this.textBoxCopyInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCopyInformation.Location = new System.Drawing.Point(3, 190);
            this.textBoxCopyInformation.Multiline = true;
            this.textBoxCopyInformation.Name = "textBoxCopyInformation";
            this.textBoxCopyInformation.ReadOnly = true;
            this.textBoxCopyInformation.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCopyInformation.Size = new System.Drawing.Size(607, 364);
            this.textBoxCopyInformation.TabIndex = 4;
            this.textBoxCopyInformation.WordWrap = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(535, 560);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCopyToClipboard
            // 
            this.buttonCopyToClipboard.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonCopyToClipboard.AutoSize = true;
            this.buttonCopyToClipboard.Location = new System.Drawing.Point(430, 560);
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.buttonCopyToClipboard.Size = new System.Drawing.Size(99, 23);
            this.buttonCopyToClipboard.TabIndex = 6;
            this.buttonCopyToClipboard.Text = "Copy to clipboard";
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            // 
            // COMComponentsExtractor
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 587);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "COMComponentsExtractor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COM+ component extraction";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Label labelServerName;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TreeView treeViewComponents;
        private System.Windows.Forms.TextBox textBoxCopyInformation;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button buttonCopyToClipboard;
    }
}