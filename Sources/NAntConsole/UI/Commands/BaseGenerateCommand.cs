using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Activa.LazyParser;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal abstract class BaseGenerateCommand : BaseUICommand
    {
        private readonly SvnExplorerSelection selection;
        private readonly string baseTemplateDir;
        private static readonly Regex reProjectName = new Regex(@".+/(?<projectName>[^/]+)/trunk", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        protected BaseGenerateCommand(SvnExplorerSelection selection, string baseTemplateDir, string commandName) : base(commandName)
        {
            this.selection = selection;
            this.baseTemplateDir = baseTemplateDir;
        }

        protected SvnExplorerSelection Selection
        {
            get { return selection; }
        }

        public sealed override CommandExecutionResult Execute()
        {
            CommandExecutionResult result = new CommandExecutionResult(this);
            try
            {
                CheckOutUICommand checkOutCommand = new CheckOutUICommand(Selection);
                checkOutCommand.DoNotCheckOutWhenLocalDirectoryExists = true;
                checkOutCommand.ReportProgress += delegate(object sender, IUICommandReportProgressEventArgs eventArgs)
                                                      {
                                                          InvokeReportProgress(eventArgs.Message);
                                                      };
                CommandExecutionResult checkOutCommandExecutionResult = checkOutCommand.Execute();
                if (checkOutCommandExecutionResult.Error != null)
                    throw checkOutCommandExecutionResult.Error;

                ParserContext context = new ParserContext();
                FillContext(context);

                DirectoryInfo targetDir = new DirectoryInfo((string)checkOutCommandExecutionResult.CommandOutput);

                TemplateHelper.GenerateTemplateHierarchy(targetDir, baseTemplateDir, context, Encoding);
                result.CommandOutput = targetDir;

                result.NewProjectFile = new FileInfo(Path.Combine(targetDir.FullName, string.Concat(GetProjectName(Selection.SvnUri), ".nant")));
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }
            return result;
        }

        protected abstract void FillContext(IParserContext context);

        protected abstract FileInfo GetNewProjectFile(DirectoryInfo targetDir);

        protected virtual Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }

        protected static string GetProjectName(string svnUri)
        {
            Match mProjectName = reProjectName.Match(svnUri);
            if (mProjectName.Success)
            {
                return mProjectName.Groups["projectName"].Value;
            } else
            {
                throw new ApplicationException(string.Format("{0}is not a valide project uri.", svnUri));
            }
        }
    }
}
