using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class CreateBranchCommand : BaseUICommand
    {
        private readonly SvnExplorerSelection svnSelection;

        public CreateBranchCommand(SvnExplorerSelection svnSelection)
            : base(@"Create branch")
        {
            this.svnSelection = svnSelection;
        }

        public override CommandExecutionResult Execute()
        {
            string leadingBranchName =
                        SvnHelper.GetNewBranchLeadingName(svnSelection.SvnUri);
            AskSingleValue newBranchName = new AskSingleValue();
            newBranchName.Text = Resources.NewBranch;
            newBranchName.Prefix = leadingBranchName.Replace(svnSelection.RepositoryUri, string.Empty);

            CommandExecutionResult result = new CommandExecutionResult(this);

            if (newBranchName.ShowDialog() == DialogResult.OK)
            {
                long revNumber = SvnHelper.RemoteBranch(svnSelection.SvnUri, leadingBranchName + newBranchName.Value);
                result.Message = string.Format(Resources.BranchCreated, leadingBranchName + newBranchName.Value, revNumber);
            }

            return result;
        }
    }
}
