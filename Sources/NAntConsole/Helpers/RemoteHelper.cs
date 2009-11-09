using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Remote;
using System.IO;

namespace CDS.Framework.Tools.NAntConsole.Helpers
{
    internal static class RemoteHelper
    {
        public static void WriteInstallDirForRemoteExecution()
        {
            RegistryKey newKey = Registry.LocalMachine.CreateSubKey(RemoteConstants.NANT_CONSOLE_INSTALL_FOLDER_REG_KEY);
            newKey.SetValue(RemoteConstants.NANT_CONSOLE_INSTALL_FOLDER_REG_VALUE, Path.GetDirectoryName(typeof(RemoteHelper).Assembly.Location));
        }
    }
}
