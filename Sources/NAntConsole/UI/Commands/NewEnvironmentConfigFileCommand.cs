using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Activa.LazyParser;
using CDS.Framework.Tools.NAntConsole.Entities;
using CDS.Framework.Tools.NAntConsole.Helpers;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class NewEnvironmentConfigFileCommand : BaseUICommand
    {
        private readonly NAntProject project;

        public NewEnvironmentConfigFileCommand(NAntProject project)
            : base(@"New environment config")
        {
            this.project = project;
        }

        public override CommandExecutionResult Execute()
        {
            AskSingleValue environmentAsk = new AskSingleValue();
            environmentAsk.Text = Resources.EnvironmentAsk;
            environmentAsk.Prefix = Resources.EnvironmentName;
            if (environmentAsk.ShowDialog() != DialogResult.OK)
            {
                return new CommandExecutionResult(this);
            }

            DirectoryInfo envDir = new DirectoryInfo(Path.Combine(project.BuildFile.DirectoryName, EnvIncludeConstants.ENV_FOLDER_NAME));
            if (!envDir.Exists)
            {
                envDir.Create();
            }

            string targetFileName = string.Format("{0}.{1}.config", project.ProjectName, environmentAsk.Value);
            FileInfo targetFileInfo = new FileInfo(Path.Combine(envDir.FullName, targetFileName));
            if (targetFileInfo.Exists)
            {
                CommandExecutionResult result = new CommandExecutionResult(this);
                result.Error = new ApplicationException(string.Format(Resources.EnvironmnentFileExists, targetFileInfo.FullName));
                return result;
            }

            ParserContext context = new ParserContext();
            context.Set("projectName", project.ProjectName);

            TemplateHelper.GenerateFile(targetFileInfo, @"EnvFile\env.config", context, Encoding.UTF8);

            return new CommandExecutionResult(this);
        }
    }
}
