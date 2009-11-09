using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class MergeBranchCommand : BaseUICommand
    {
        private readonly SvnExplorerSelection svnSelection;

        public MergeBranchCommand(SvnExplorerSelection svnSelection)
            : base(@"Merge branch")
        {
            this.svnSelection = svnSelection;
        }

        public override CommandExecutionResult Execute()
        {
            MergeBranchInfo mergeBranchInfo = SvnHelper.GetMergeBranchInfo(svnSelection.SvnUri);
            MergeBranchWizard wizard = new MergeBranchWizard();
            CommandExecutionResult result = new CommandExecutionResult(this);
            try
            {
                if (wizard.ShowDialog() == DialogResult.OK)
                {
                    MergeBranchExecute mergeBranchExecute = new MergeBranchExecute();
                    mergeBranchExecute.Source = svnSelection;
                    mergeBranchExecute.MergeBranchInfo = mergeBranchInfo;
                    mergeBranchExecute.MergeChoice = wizard.Choice;
                    mergeBranchExecute.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return result;
        }
    }
}
