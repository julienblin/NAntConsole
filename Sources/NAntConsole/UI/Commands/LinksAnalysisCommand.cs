using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Entities;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class LinksAnalysisCommand : BaseUICommand
    {
        private readonly Form mainForm;
        private readonly NAntProject nantProject;

        public LinksAnalysisCommand(Form mainForm, NAntProject nantProject)
            : base(@"Links analysis")
        {
            this.mainForm = mainForm;
            this.nantProject = nantProject;
        }

        public override CommandExecutionResult Execute()
        {
            CommandExecutionResult result = new CommandExecutionResult(this);
            try
            {
                if (!SvnHelper.IsLocalFolderUnderSvnControl(nantProject.BuildFile.Directory.FullName))
                {
                    MessageBox.Show(mainForm, Resources.LocalFolderNotUnderSourceControl, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return result;
                }

                if (MessageBox.Show(mainForm, Resources.LinkAnalysisConfirm, Resources.ConfirmationCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    SvnExplorer svnExplorer = new SvnExplorer();
                    svnExplorer.ReadOnly = true;
                    if (svnExplorer.ShowDialog(mainForm) == DialogResult.OK)
                    {
                        LinkAnalysisProgress progress = new LinkAnalysisProgress();
                        progress.SvnExplorerSelection = svnExplorer.GetSvnExplorerSelection();
                        progress.SearchProjectUri = SvnHelper.GetUriFromWorkingCopy(nantProject.BuildFile.Directory.FullName);
                        progress.ShowDialog(mainForm);
                        LinksList linksList = new LinksList();
                        linksList.Prefix = Resources.DependentProjects;
                        linksList.Project = SvnHelper.GetUriFromWorkingCopy(nantProject.BuildFile.Directory.FullName);
                        foreach (string link in progress.Result)
                        {
                            linksList.AddLink(link, string.Empty);
                        }
                        linksList.ShowDialog(mainForm);
                    }
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
