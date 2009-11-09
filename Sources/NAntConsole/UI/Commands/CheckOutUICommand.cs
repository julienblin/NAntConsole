using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CDS.Framework.Tools.NAntConsole.Configuration;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class CheckOutUICommand : BaseUICommand
    {
        private readonly SvnExplorerSelection selection;

        public CheckOutUICommand(SvnExplorerSelection selection)
            : base("Checkout")
        {
            this.selection = selection;
        }

        private bool doNotCheckOutWhenLocalDirectoryExists = false;

        public bool DoNotCheckOutWhenLocalDirectoryExists
        {
            get { return doNotCheckOutWhenLocalDirectoryExists; }
            set { doNotCheckOutWhenLocalDirectoryExists = value; }
        }

        public override CommandExecutionResult Execute()
        {
            CommandExecutionResult result = new CommandExecutionResult(this);
            try
            {
                string checkOutPath = Path.Combine(NAntConsoleConfigurationSection.GetCheckOutDirectory(),
                                                   SvnHelper.GetPathFromSvnUri(selection.SvnUriWithoutRepository));

                result.CommandOutput = checkOutPath;

                if (DoNotCheckOutWhenLocalDirectoryExists && Directory.Exists(checkOutPath))
                {
                    result.Error = new ApplicationException(string.Format(Resources.ErrorCheckOutDirectoryExists, checkOutPath));
                    return result;
                }

                InvokeReportProgress(string.Format(Resources.CheckingOut, selection.SvnUri, checkOutPath));

                SvnHelper.CheckOut(selection.SvnUri, checkOutPath, delegate(SvnExecutionProgressEventArgs args)
                                                                               {
                                                                                   InvokeReportProgress(args.Message);
                                                                               });

                string[] nantFiles = Directory.GetFiles(checkOutPath, "*.nant");
                if (nantFiles.Length > 0)
                {
                    result.NewProjectFile = new FileInfo(nantFiles[0]);
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
