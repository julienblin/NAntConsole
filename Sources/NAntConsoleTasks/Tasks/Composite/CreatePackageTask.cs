using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using NAnt.Compression.Tasks;
using NAnt.Compression.Types;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;
using NAnt.Core.Types;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite
{
    [TaskName("create-package")]
    public class CreatePackageTask : BaseCompositeTask
    {
        private DirectoryInfo dir;
        [TaskAttribute("dir", Required = true)]
        public DirectoryInfo Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        private FileInfo package;
        [TaskAttribute("package", Required = true)]
        public FileInfo Package
        {
            get { return package; }
            set { package = value; }
        }

        protected override void ExecuteTask()
        {
            DirectoryInfo envDirectory = new DirectoryInfo(Project.GetFullPath(CompositeConstants.ENV_DIRECTORY_NAME));
            if (envDirectory.Exists)
            {
                Log(Level.Info, Resources.CreatePackageCopyEnvDirectory);
                DirectoryInfo targetDirectory = new DirectoryInfo(Path.Combine(Dir.FullName, CompositeConstants.ENV_DIRECTORY_NAME));
                if (targetDirectory.Exists)
                {
                    targetDirectory.Delete();
                }

                // Copying Environment folder.
                CopyTask copyTask = base.CreateTask<CopyTask>();
                FileSet srcFileSet = new FileSet();
                srcFileSet.DefaultExcludes = true;
                srcFileSet.BaseDirectory = envDirectory;
                srcFileSet.Includes.Add(@"**/*");
                copyTask.CopyFileSet = srcFileSet;
                copyTask.ToDirectory = targetDirectory;
                copyTask.Execute();
            }

            File.Copy(Project.BuildFileLocalName,
                      Path.Combine(Dir.FullName, CompositeConstants.DEPLOY_FILE_NAME), true);

            // Create NAntConsole version marker
            Version nantConsoleVersion = GetType().Assembly.GetName().Version;
            string fileVersionPath = Path.Combine(Dir.FullName, CompositeConstants.NANTCONSOLE_VERSION_FILE_NAME);
            if(File.Exists(fileVersionPath))
            {
                File.Delete(fileVersionPath);
            }
            using(StreamWriter writer = File.CreateText(fileVersionPath))
            {
                writer.Write(nantConsoleVersion.ToString());
            }

            // Write version property.
            if (Properties.Contains(EnvIncludeConstants.DEFAULT_VERSION_PROPERTY_NAME))
            {
                using(StreamWriter writer = File.CreateText(Path.Combine(Dir.FullName, EnvIncludeConstants.VERSION_FILENAME)))
                {
                    writer.Write(Properties[EnvIncludeConstants.DEFAULT_VERSION_PROPERTY_NAME]);
                }
            }

            if (Package.Exists)
            {
                Package.Delete();
            }

            ZipTask zipTask = CreateTask<ZipTask>();
            zipTask.ZipFile = Package;
            zipTask.IncludeEmptyDirs = true;
            ZipFileSet files = new ZipFileSet();
            files.BaseDirectory = Dir;
            files.Includes.Add("**/*");
            zipTask.ZipFileSets.Add(files);
            zipTask.Execute();
        }
    }
}
