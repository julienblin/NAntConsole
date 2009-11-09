using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Helpers;

namespace CDS.Framework.Tools.NAntConsole.UI.Commands
{
    internal class CheckForUpdatesCommand : BaseUICommand
    {
        public CheckForUpdatesCommand()
            : base(@"Check for updates")
        {
        }

        public override CommandExecutionResult Execute()
        {
            CommandExecutionResult result = new CommandExecutionResult(this);
            result.CommandOutput = false;
            UpdateInfo updateInfo = UpdateHelper.CheckNewVersionAvailability(true);
            if (updateInfo != null)
            {
                if (
                    MessageBox.Show(string.Format(Resources.NewUpdateFound, updateInfo.Version, updateInfo.Location.FullName), Resources.NewUpdateCaption,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        UpdateHelper.Update(updateInfo);
                        Application.Exit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        result.Error = ex;
                    }
                    finally
                    {
                        result.CommandOutput = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(Resources.NewUpdateNotFound, Resources.UpdateCaption, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            return result;
        }
    }
}
