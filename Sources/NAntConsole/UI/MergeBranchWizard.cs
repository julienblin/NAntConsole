using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class MergeBranchWizard : Form
    {
        public MergeBranchWizard()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        private void ConnectEventHandlers()
        {
            buttonNext.Click += OnButtonNextClick;
        }

        private void OnButtonNextClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public MergeChoice Choice
        {
            get {
                return radioButtonUpdateBranch.Checked ? MergeChoice.Update : MergeChoice.Reintegrate;
            }
            set
            {
                switch (value)
                {
                    case MergeChoice.Reintegrate:
                        radioButtonReintegrateBranch.Checked = true;
                        break;
                    case MergeChoice.Update:
                        radioButtonUpdateBranch.Checked = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value");
                }
            }
        }

        public enum MergeChoice
        {
            Reintegrate,
            Update
        }
    }
}