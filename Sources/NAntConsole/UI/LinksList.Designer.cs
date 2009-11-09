namespace CDS.Framework.Tools.NAntConsole.UI
{
    partial class LinksList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinksList));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.labelPrefix = new System.Windows.Forms.Label();
            this.labelProject = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.listViewLinks = new System.Windows.Forms.ListView();
            this.columnHeaderUrl = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.labelProject, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelPrefix, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOK, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.listViewLinks, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(648, 383);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // labelPrefix
            // 
            this.labelPrefix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPrefix.AutoSize = true;
            this.labelPrefix.Location = new System.Drawing.Point(3, 13);
            this.labelPrefix.Name = "labelPrefix";
            this.labelPrefix.Size = new System.Drawing.Size(39, 13);
            this.labelPrefix.TabIndex = 0;
            this.labelPrefix.Text = "Prefix :";
            // 
            // labelProject
            // 
            this.labelProject.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelProject.AutoSize = true;
            this.labelProject.Location = new System.Drawing.Point(48, 13);
            this.labelProject.Name = "labelProject";
            this.labelProject.Size = new System.Drawing.Size(40, 13);
            this.labelProject.TabIndex = 1;
            this.labelProject.Text = "Project";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelMain.SetColumnSpan(this.buttonOK, 2);
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(286, 357);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // listViewLinks
            // 
            this.listViewLinks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderUrl,
            this.columnHeaderName});
            this.tableLayoutPanelMain.SetColumnSpan(this.listViewLinks, 2);
            this.listViewLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLinks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLinks.Location = new System.Drawing.Point(3, 43);
            this.listViewLinks.Name = "listViewLinks";
            this.listViewLinks.Size = new System.Drawing.Size(642, 308);
            this.listViewLinks.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewLinks.TabIndex = 3;
            this.listViewLinks.UseCompatibleStateImageBehavior = false;
            this.listViewLinks.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderUrl
            // 
            this.columnHeaderUrl.Text = "Url";
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "";
            // 
            // LinksList
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(654, 389);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "LinksList";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Link list";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Label labelProject;
        private System.Windows.Forms.Label labelPrefix;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ListView listViewLinks;
        private System.Windows.Forms.ColumnHeader columnHeaderUrl;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
    }
}