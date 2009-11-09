using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Activa.LazyParser;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class NewNAntFileCommand : BaseUICommand
    {
        private readonly string filename;

        public NewNAntFileCommand(string filename) : base(@"New nant file")
        {
            this.filename = filename;
        }

        public override CommandExecutionResult Execute()
        {
            string projectName = Path.GetFileNameWithoutExtension(filename);
            ParserContext context = new ParserContext();
            context.Set("projectName", projectName);

            FileInfo targetFile = new FileInfo(filename);
            TemplateHelper.GenerateFile(targetFile, @"NAntFile\nantfile.nant", context, Encoding.UTF8);

            CommandExecutionResult result = new CommandExecutionResult(this);
            result.NewProjectFile = targetFile;
            return result;
        }
    }
}
