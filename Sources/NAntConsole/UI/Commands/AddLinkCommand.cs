using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Entities;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class AddLinkCommand : BaseUICommand
    {
        private const string EXTERNALS_PROPERTY_NAME = @"svn:externals";
        private const string BIN_DIRECTORY = @"Bin";
        private const string DEP_DIRECTORY = @"Dependencies";
        private const string ENV_DIRECTORY = @"Environment";

        private readonly Form mainForm;
        private readonly NAntProject nantProject;

        public AddLinkCommand(Form mainForm, NAntProject nantProject)
            : base(@"Add link")
        {
            this.mainForm = mainForm;
            this.nantProject = nantProject;
        }

        public override CommandExecutionResult Execute()
        {
            CommandExecutionResult result = new CommandExecutionResult(this);
            result.CommandOutput = false;
            if (!SvnHelper.IsLocalFolderUnderSvnControl(nantProject.BuildFile.Directory.FullName))
            {
                MessageBox.Show(mainForm, Resources.LocalFolderNotUnderSourceControl, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result;
            }

            try
            {
                SvnExplorer svnExplorer = new SvnExplorer();
                svnExplorer.TrunkSubTagOrSubBranchOnly = true;
                if (svnExplorer.ShowDialog(mainForm) == DialogResult.OK)
                {
                    SvnExplorerSelection svnExplorerSelection = svnExplorer.GetSvnExplorerSelection();
                    AddLinkSelection addLinkSelection = new AddLinkSelection();
                    if (addLinkSelection.ShowDialog(mainForm) == DialogResult.OK)
                    {
                        string svnProjectUri = SvnHelper.GetUriFromWorkingCopy(nantProject.BuildFile.Directory.FullName);
                        string externalPropInit = SvnHelper.GetProperty(nantProject.BuildFile.Directory.FullName, EXTERNALS_PROPERTY_NAME);
                        string externalProp = externalPropInit ?? string.Empty;

                        if (addLinkSelection.BinLinkChecked)
                        {
                            string targetBinUri;
                            if (svnProjectUri.StartsWith(svnExplorerSelection.RepositoryUri))
                            {
                                targetBinUri = string.Concat("^", svnExplorerSelection.SvnUriWithoutRepository, BIN_DIRECTORY);
                            }
                            else
                            {
                                targetBinUri = string.Concat(svnExplorerSelection.SvnUri, BIN_DIRECTORY);
                            }
                            targetBinUri = targetBinUri.Replace("%20", " ");

                            string targetFolder = string.Concat(DEP_DIRECTORY, SvnHelper.StripTagsTrunkBranches(string.Concat(svnExplorerSelection.SvnUriWithoutRepository, BIN_DIRECTORY)));

                            string externalPropAdded = string.Concat(targetBinUri, " ", targetFolder);
                            if(!externalProp.Contains(externalPropAdded))
                            {
                                externalProp = string.Concat(externalProp, string.IsNullOrEmpty(externalProp) ? string.Empty : "\n", externalPropAdded);
                            }
                        }

                        if (addLinkSelection.EnvLinkChecked)
                        {
                            string targetEnvUri;
                            if (svnProjectUri.StartsWith(svnExplorerSelection.RepositoryUri))
                            {
                                targetEnvUri = string.Concat("^", svnExplorerSelection.SvnUriWithoutRepository, ENV_DIRECTORY);
                            }
                            else
                            {
                                targetEnvUri = string.Concat(svnExplorerSelection.SvnUri, ENV_DIRECTORY);
                            }
                            targetEnvUri = targetEnvUri.Replace("%20", " ");

                            string externalPropAdded = string.Concat(targetEnvUri, " ", ENV_DIRECTORY);
                            if (!externalProp.Contains(externalPropAdded))
                            {
                                externalProp = string.Concat(externalProp, string.IsNullOrEmpty(externalProp) ? string.Empty : "\n", externalPropAdded);
                            }
                        }

                        if (!externalProp.Equals(externalPropInit, StringComparison.InvariantCulture))
                        {
                            SvnHelper.SetProperty(nantProject.BuildFile.Directory.FullName, EXTERNALS_PROPERTY_NAME, externalProp);
                            result.CommandOutput = true;
                        }
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
