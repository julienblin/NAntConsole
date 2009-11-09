using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public class VisualStudioIntegration
    {
        private const string VS_APPLICATION_OPEN_COMMAND_KEY = @"SOFTWARE\Classes\Applications\devenv.exe\shell\open\command";
        private const string NANT_SCHEMA_PATH = @"Schemas\nant.xsd";
        private const string VS_XML_SCHEMAS_RELATIVE_TO_DEVENV_DIRECTORY = @"..\..\Xml\Schemas";

        static readonly Regex reVSExe = new Regex("^\"?(?<exe>[^\"%]+)\"?", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);

        private bool? isVisualStudioInstalled = null;
        private string vsPath;

        public bool IsVisualStudioInstalled()
        {
            if (!isVisualStudioInstalled.HasValue)
            {
                isVisualStudioInstalled = (Registry.LocalMachine.OpenSubKey(VS_APPLICATION_OPEN_COMMAND_KEY) != null);
                if (isVisualStudioInstalled.Value)
                {
                    vsPath = (string)Registry.LocalMachine.OpenSubKey(VS_APPLICATION_OPEN_COMMAND_KEY).GetValue(null);
                }
            }
            return isVisualStudioInstalled.Value;
        }

        public void OpenFileInVS(string name)
        {
            if (string.IsNullOrEmpty(vsPath))
                return;

            Match vsExe = reVSExe.Match(vsPath);
            CopySchemaIfPossible(Path.GetDirectoryName(vsExe.Groups["exe"].Value));
            if (vsExe.Success)
            {
                FileInfo[] envFiles = GetEnvFilesFromNantFileName(name);
                StringBuilder sbArgs = new StringBuilder();
                sbArgs.AppendFormat("\"{0}\" ", name);
                foreach (FileInfo envFile in envFiles)
                {
                    sbArgs.AppendFormat("\"{0}\" ", envFile.FullName);
                }
                Process.Start(vsExe.Groups["exe"].Value, sbArgs.ToString());
            }
        }

        private FileInfo[] GetEnvFilesFromNantFileName(string name)
        {
            FileInfo nantFile = new FileInfo(name);
            if (nantFile.Exists)
            {
                DirectoryInfo envDir = new DirectoryInfo(Path.Combine(nantFile.DirectoryName, CompositeConstants.ENV_DIRECTORY_NAME));
                if (envDir.Exists)
                {
                    return envDir.GetFiles("*.config");
                }
            }
            return new FileInfo[] { };
        }

        private void CopySchemaIfPossible(string devEnvDirectoryName)
        {
            try
            {
                string nantSchemaPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                     NANT_SCHEMA_PATH);
                if (File.Exists(nantSchemaPath))
                {
                    DirectoryInfo vsSchemaDirInfo = new DirectoryInfo(Path.Combine(devEnvDirectoryName, VS_XML_SCHEMAS_RELATIVE_TO_DEVENV_DIRECTORY));
                    if (vsSchemaDirInfo.Exists)
                    {
                        File.Copy(nantSchemaPath, Path.Combine(vsSchemaDirInfo.FullName, @"nant.xsd"), true);
                    }
                }
            }
            catch { }
        }
    }
}
