using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Activa.LazyParser;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class GenerateEmptyProjectCommand : BaseGenerateCommand
    {
        public GenerateEmptyProjectCommand(SvnExplorerSelection selection)
            : base(selection, @"EmptyProject", @"Generate empty project")
        {
        }

        protected override void FillContext(IParserContext context)
        {
            context.Set("projectName", GetProjectName(Selection.SvnUri));
        }

        protected override FileInfo GetNewProjectFile(DirectoryInfo targetDir)
        {
            return new FileInfo(Path.Combine(targetDir.FullName, string.Concat(GetProjectName(Selection.SvnUri), ".nant")));
        }
    }
}
