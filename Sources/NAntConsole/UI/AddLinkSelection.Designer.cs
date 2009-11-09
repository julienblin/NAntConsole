namespace CDS.Framework.Tools.NAntConsole.UI
{
    partial class AddLinkSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLinkSelection));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxBin = new System.Windows.Forms.CheckBox();
            this.checkBoxEnv = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.checkBoxBin, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.checkBoxEnv, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOK, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.buttonCancel, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(413, 127);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // checkBoxBin
            // 
            this.checkBoxBin.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxBin.AutoSize = true;
            this.checkBoxBin.Checked = true;
            this.checkBoxBin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanelMain.SetColumnSpan(this.checkBoxBin, 2);
            this.checkBoxBin.Location = new System.Drawing.Point(3, 16);
            this.checkBoxBin.Name = "checkBoxBin";
            this.checkBoxBin.Size = new System.Drawing.Size(175, 17);
            this.checkBoxBin.TabIndex = 0;
            this.checkBoxBin.Text = "Add a link Bin -> Dependencies";
            this.checkBoxBin.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnv
            // 
            this.checkBoxEnv.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxEnv.AutoSize = true;
            this.checkBoxEnv.Checked = true;
            this.checkBoxEnv.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanelMain.SetColumnSpan(this.checkBoxEnv, 2);
            this.checkBoxEnv.Location = new System.Drawing.Point(3, 65);
            this.checkBoxEnv.Name = "checkBoxEnv";
            this.checkBoxEnv.Size = new System.Drawing.Size(209, 17);
            this.checkBoxEnv.TabIndex = 1;
            this.checkBoxEnv.Text = "Add a link Environment -> Environment";
            this.checkBoxEnv.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonOK.Location = new System.Drawing.Point(335, 101);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Next >>";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(254, 101);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // AddLinkSelection
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(419, 133);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLinkSelection";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add project link...";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.CheckBox checkBoxBin;
        private System.Windows.Forms.CheckBox checkBoxEnv;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}