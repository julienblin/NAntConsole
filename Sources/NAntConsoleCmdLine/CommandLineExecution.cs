using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CDS.Framework.Tools.NAntConsole.Entities;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite;
using CDS.Framework.Tools.NAntConsole.Helpers;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks;
using CDS.Framework.Tools.NAntConsole.UI;
using System.Data;

namespace CDS.Framework.Tools.NAntConsoleCmdLine
{
    public static class CommandLineExecution
    {
        public static int Execute(FileInfo deployFile, string targetName)
        {
            DirectoryInfo packageDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
            if (packageDir.Exists)
            {
                packageDir.Delete(true);
            }

            int returnCode = -1;

            try
            {
                packageDir.Create();
                Console.WriteLine(string.Format("[ExecutionInfo] Machine : {0} - Username : {1}\\{2} - Time : {3:yyyy-MM-dd HH:mm}", Environment.MachineName, Environment.UserDomainName, Environment.UserName, DateTime.Now));
                Console.WriteLine(string.Format("Extracting package {0} into {1}...", deployFile.FullName, packageDir.FullName));
                ZipHelper.CheckNAntConsoleVersion(deployFile);
                ZipHelper.UnZip(deployFile, packageDir);

                Version packageVersion;

                if (File.Exists(Path.Combine(packageDir.FullName, EnvIncludeConstants.VERSION_FILENAME)))
                {
                    try
                    {
                        packageVersion = new Version(File.ReadAllText(Path.Combine(packageDir.FullName, EnvIncludeConstants.VERSION_FILENAME)));
                    }
                    catch
                    {
                    }
                }

                NAntProject nantProject = GetNantProject(packageDir);

                returnCode = NAntHelper.ExecuteNant(nantProject, targetName, delegate(NAntExecutionProgressEventArgs progressArgs)
                {
                    Console.WriteLine(progressArgs.Message);
                });
            }
            catch (VersionNotFoundException versionEx)
            {
                Console.WriteLine(string.Format("Fatal error : {0}.", versionEx));
            }
            catch (Exception ex)
            {
               Console.WriteLine(string.Format("Fatal error : {0}.", ex));
            }
            finally
            {
                packageDir.Delete(true);
            }
            return returnCode;
        }
        
        private static NAntProject GetNantProject(DirectoryInfo packageDir)
        {
            FileInfo nantFile = new FileInfo(Path.Combine(packageDir.FullName, CompositeConstants.DEPLOY_FILE_NAME));
            return NAntHelper.LoadProject(nantFile);
        }
    }
}
