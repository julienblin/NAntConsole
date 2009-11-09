using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CDS.Framework.Tools.NAntConsole.Helpers;
using CDS.Framework.Tools.NAntConsole.UI;

namespace CDS.Framework.Tools.NAntConsole
{
    static class Program
    {
        private const string VIEW_INSTALL_TARGET_NAME = @"ViewInstall";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RemoteHelper.WriteInstallDirForRemoteExecution();

            UpdateInfo updateInfo = UpdateHelper.CheckNewVersionAvailability();

            bool exitBecauseOfUpdate = false;

            if (updateInfo != null)
            {
                if (
                    MessageBox.Show(string.Format(Resources.NewUpdateFound, updateInfo.Version, updateInfo.Location.FullName), Resources.NewUpdateCaption,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        UpdateHelper.Update(updateInfo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        exitBecauseOfUpdate = true;
                    }
                }
            }
            else
            {
                UpdateHelper.CleanUpArtifactsOnRelaunch();
            }

            if (!exitBecauseOfUpdate)
            {
                Form form = null;

                if (args.Length == 1)
                {
                    if (File.Exists(args[0]))
                    {
                        FileInfo parameter = new FileInfo(args[0]);
                        switch (parameter.Extension)
                        {
                            case ".nant":
                                form = new MainForm();
                                ((MainForm)form).ProjectFile = parameter;
                                break;
                            case ".deploy":
                                form = new DisplayOnly();
                                ((DisplayOnly)form).DeployFile = parameter;
                                break;
                        }
                    }
                }

                if (args.Length >= 2)
                {
                    if (File.Exists(args[0]))
                    {
                        FileInfo parameter = new FileInfo(args[0]);
                        if (parameter.Extension.Equals(".deploy", StringComparison.InvariantCultureIgnoreCase))
                        {

                            if (args[1].Equals(VIEW_INSTALL_TARGET_NAME, StringComparison.InvariantCultureIgnoreCase))
                            {
                                form = new ViewInstall();
                                ((ViewInstall)form).DeployFile = parameter;
                            }
                            else
                            {
                                form = new DisplayOnly();
                                ((DisplayOnly)form).DeployFile = parameter;
                                ((DisplayOnly)form).TargetName = args[1];
                                if (args.Length == 3)
                                {
                                    if (args[2].Equals("/y"))
                                    {
                                        ((DisplayOnly)form).ConfirmInstallOrUninstall = false;
                                    }
                                }
                            }
                        }
                    }
                }

                if (form == null)
                {
                    form = new MainForm();
                }

                Application.Run(form);
            }
        }
    }
}