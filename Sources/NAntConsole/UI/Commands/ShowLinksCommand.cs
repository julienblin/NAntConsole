using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Entities;
using CDS.Framework.Tools.NAntConsole.Helpers;
using System.IO;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class ShowLinksCommand : BaseUICommand
    {
        private readonly Form mainForm;
        private readonly NAntProject nantProject;

        public ShowLinksCommand(Form mainForm, NAntProject nantProject) : base(@"Show links")
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

                string externalProp = SvnHelper.GetProperty(nantProject.BuildFile.Directory.FullName, SvnHelper.EXTERNALS_PROPERTY_NAME) ?? string.Empty;
                LinksList linksList = new LinksList();
                linksList.Prefix = Resources.ProjectDependencies;
                linksList.Project = SvnHelper.GetUriFromWorkingCopy(nantProject.BuildFile.Directory.FullName);
                List<string> links = new List<string>();
                using(StringReader reader = new StringReader(externalProp))
                {
                    string external = reader.ReadLine();
                    while(external != null)
                    {
                        string[] splittedExternal = external.Split(' ');
                        if (splittedExternal.Length == 2)
                        {
                            linksList.AddLink(splittedExternal[0].Replace(" ", "%20"), splittedExternal[1]);
                        }
                        external = reader.ReadLine();
                    }
                }
                linksList.ShowDialog(mainForm);
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }
            return result;
        }
    }
}
