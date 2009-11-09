namespace CDS.Framework.Tools.NAntConsole.UI
{
    partial class MergeBranchWizard
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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.radioButtonReintegrateBranch = new System.Windows.Forms.RadioButton();
            this.radioButtonUpdateBranch = new System.Windows.Forms.RadioButton();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.radioButtonUpdateBranch, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.radioButtonReintegrateBranch, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonNext, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.buttonCancel, 2, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(607, 112);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // radioButtonReintegrateBranch
            // 
            this.radioButtonReintegrateBranch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioButtonReintegrateBranch.AutoSize = true;
            this.radioButtonReintegrateBranch.Checked = true;
            this.tableLayoutPanelMain.SetColumnSpan(this.radioButtonReintegrateBranch, 3);
            this.radioButtonReintegrateBranch.Location = new System.Drawing.Point(3, 12);
            this.radioButtonReintegrateBranch.Name = "radioButtonReintegrateBranch";
            this.radioButtonReintegrateBranch.Size = new System.Drawing.Size(476, 17);
            this.radioButtonReintegrateBranch.TabIndex = 0;
            this.radioButtonReintegrateBranch.TabStop = true;
            this.radioButtonReintegrateBranch.Text = "Reintegrate branch into its initial copy - Used to close a branch and copy modifi" +
                "cation into trunk.";
            this.radioButtonReintegrateBranch.UseVisualStyleBackColor = true;
            // 
            // radioButtonUpdateBranch
            // 
            this.radioButtonUpdateBranch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioButtonUpdateBranch.AutoSize = true;
            this.tableLayoutPanelMain.SetColumnSpan(this.radioButtonUpdateBranch, 3);
            this.radioButtonUpdateBranch.Location = new System.Drawing.Point(3, 53);
            this.radioButtonUpdateBranch.Name = "radioButtonUpdateBranch";
            this.radioButtonUpdateBranch.Size = new System.Drawing.Size(595, 17);
            this.radioButtonUpdateBranch.TabIndex = 1;
            this.radioButtonUpdateBranch.Text = "Update branch with new information from its inital copy - Used to keep a branch u" +
                "p-to-date from latest trunk modifications.";
            this.radioButtonUpdateBranch.UseVisualStyleBackColor = true;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonNext.Location = new System.Drawing.Point(448, 85);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "Next >>";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCancel.Location = new System.Drawing.Point(529, 85);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // MergeBranchWizard
            // 
            this.AcceptButton = this.buttonNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(607, 112);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MergeBranchWizard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Merge branch wizard";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.RadioButton radioButtonUpdateBranch;
        private System.Windows.Forms.RadioButton radioButtonReintegrateBranch;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
    }
}