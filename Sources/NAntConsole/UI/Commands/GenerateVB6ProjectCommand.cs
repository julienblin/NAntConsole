using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Activa.LazyParser;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class GenerateVB6ProjectCommand : BaseGenerateCommand
    {
        public GenerateVB6ProjectCommand(SvnExplorerSelection selection)
            : base(selection, @"VB6Project", @"Generate VB6 project")
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

        protected override Encoding Encoding
        {
            get { return Encoding.GetEncoding("ISO-8859-1"); }
        } 
    }
}
