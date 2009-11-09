using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Net
{
    [TaskName("aspnet-compile")]
    [ProgramLocation(LocationType.FrameworkSdkDir)]
    public class ASPNetCompileTask : ExternalProgramBase
    {
        private DirectoryInfo sourceDir;
        [TaskAttribute("srcDir", Required = true)]
        public DirectoryInfo SourceDir
        {
            get { return sourceDir; }
            set { sourceDir = value; }
        }

        private DirectoryInfo targetDir;
        [TaskAttribute("targetDir", Required = true)]
        public DirectoryInfo TargetDir
        {
            get { return targetDir; }
            set { targetDir = value; }
        }

        private bool updatable;
        [TaskAttribute("updatable")]
        public bool Updatable
        {
            get { return updatable; }
            set { updatable = value; }
        }

        private bool debug;
        [TaskAttribute("debug")]
        public bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }

        private bool fixedNames;
        [TaskAttribute("fixedNames")]
        public bool FixedNames
        {
            get { return fixedNames; }
            set { fixedNames = value; }
        }

        public override string ExeName
        {
            get
            {
                return "aspnet_compiler";
            }
        }
        
        public override string ProgramArguments
        {
            get
            {
                StringBuilder sbArgs = new StringBuilder();
                sbArgs.Append("-v /");
                sbArgs.AppendFormat(" -p \"{0}\"", SourceDir.FullName);
                
                if (Updatable)
                    sbArgs.Append(" -u");

                if (Debug)
                    sbArgs.Append(" -d");

                if (FixedNames)
                    sbArgs.Append(" -fixednames");

                sbArgs.Append(" -nologo");

                sbArgs.AppendFormat(" \"{0}\"", TargetDir.FullName);

                return sbArgs.ToString();
            }
        }

        protected override void ExecuteTask()
        {
            Log(Level.Info, string.Format(Resources.ASPNetPreCompiling, SourceDir, TargetDir));

            // Delete Target directory
            if (Directory.Exists(TargetDir.FullName))
            {
                try
                {
                    Directory.Delete(TargetDir.FullName, true);
                }
                catch (Exception)
                {
                }
            }

            base.ExecuteTask();

            // Delete the remaining csproj / csproj.user files that make their way to the compiled site...
            FileInfo[] csProjFiles = TargetDir.GetFiles("*.csproj*", SearchOption.AllDirectories);
            foreach (FileInfo file in csProjFiles)
            {
                File.Delete(file.FullName);
            }
        }
    }
}
