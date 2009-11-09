using System;
using System.Collections.Generic;
using System.Text;
using CDS.Framework.Tools.NAntConsole.Entities;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class UpdateCommand : BaseUICommand
    {
        private readonly NAntProject nAntProject;

        public UpdateCommand(NAntProject nAntProject) : base(@"Update")
        {
            this.nAntProject = nAntProject;
        }

        public override CommandExecutionResult Execute()
        {
            CommandExecutionResult result = new CommandExecutionResult(this);
            try
            {
                InvokeReportProgress(string.Format(Resources.Updating, nAntProject.BuildFile.Directory.FullName));
                SvnHelper.Update(nAntProject.BuildFile.Directory.FullName, delegate(SvnExecutionProgressEventArgs args)
                                                                               {
                                                                                   InvokeReportProgress(args.Message);
                                                                               });
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }
            return result;
        }
    }
}
