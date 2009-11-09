using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.NAntContrib;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;
using NAnt.Core.Util;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite
{
    [TaskName("build-component")]
    public class BuildComponentTask : BaseCompositeTask
    {
        private const string DEFAULT_VB6_OUTDIR = @"Bin";
        private const string VB6_DEBUG_CONDITIONAL = @"RunMode=0";
        private const string VB6_RELEASE_CONDITIONAL = @"RunMode=2";

        private const string NET_DEBUG_CONFIGURATION = @"Debug";
        private const string NET_RELEASE_CONFIGURATION = @"Release";

        private FileInfo projectFile;
        [TaskAttribute("project", Required = true)]
        public FileInfo ProjectFile
        {
            get { return projectFile; }
            set { projectFile = value; }
        }

        [TaskAttribute("configuration", Required = true)]
        public BuildConfiguration BuildConfiguration
        {
            get { return buildConfiguration; }
            set { buildConfiguration = value; }
        }

        private BuildConfiguration buildConfiguration;

        protected override void ExecuteTask()
        {
            if (!ProjectFile.Exists)
            {
                throw new BuildException(string.Format(Resources.BuildComponentFileNotFound, ProjectFile.FullName));
            }

            ExecuteLinkedTarget(ProjectFile, CompositeConstants.PRE_BUILD_TARGET_PREFIX);

            switch (ProjectFile.Extension)
            {
                case ".vbp":
                    BuildVB(ProjectFile);
                    break;
                case ".sln":
                    BuildNet(ProjectFile);
                    break;
                default:
                    throw new BuildException(string.Format(Resources.BuildComponentNotSupported, ProjectFile.FullName));
            }

            ExecuteLinkedTarget(ProjectFile, CompositeConstants.POST_BUILD_TARGET_PREFIX);
        }

        private void ExecuteLinkedTarget(FileInfo componentFileInfo, string prefix)
        {
            string buildTaskName = string.Concat(prefix, componentFileInfo.Name);
            Target target = FindTarget(buildTaskName);
            if (target != null)
            {
                Log(Level.Info, string.Format(Resources.BuildComponentExecuteTarget, target.Name));
                target.Parent = this;
                target.Execute();
            }
        }

        private Target FindTarget(string name)
        {
            foreach (Target target in Project.Targets)
            {
                if (target.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return target;
                }
            }
            return null;
        }

        private void BuildVB(FileInfo component)
        {
            Vb6Task vb6Task = CreateTask<Vb6Task>();
            vb6Task.ProjectFile = component;
            vb6Task.OutDir = new DirectoryInfo(Path.Combine(base.Project.BaseDirectory, DEFAULT_VB6_OUTDIR));
            vb6Task.CheckReferences = false;
            switch (BuildConfiguration)
            {
                case BuildConfiguration.debug:
                    vb6Task.Conditionals = VB6_DEBUG_CONDITIONAL;
                    break;
                case BuildConfiguration.release:
                    vb6Task.Conditionals = VB6_RELEASE_CONDITIONAL;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            vb6Task.Execute();
            string vb6ArtifactPath = Path.Combine(vb6Task.OutDir.FullName, string.Concat(Path.GetFileNameWithoutExtension(vb6Task.ProjectFile.Name), ".dll"));
            string vb6ArtifactExpPath = Path.Combine(vb6Task.OutDir.FullName, string.Concat(Path.GetFileNameWithoutExtension(vb6Task.ProjectFile.Name), ".exp"));
            string vb6ArtifactLibPath = Path.Combine(vb6Task.OutDir.FullName, string.Concat(Path.GetFileNameWithoutExtension(vb6Task.ProjectFile.Name), ".lib"));


            if (!File.Exists(vb6ArtifactPath))
            {
                throw new BuildException(string.Format(Resources.BuildComponentMissingVB6Artifact, vb6ArtifactPath));
            }

            // Deletes .exp and .lib produced by vb6 command-line compilation.
            if(File.Exists(vb6ArtifactExpPath))
                File.Delete(vb6ArtifactExpPath);
            if (File.Exists(vb6ArtifactLibPath))
                File.Delete(vb6ArtifactLibPath);

            COMRegisterTask comRegisterTask = CreateTask<COMRegisterTask>();
            comRegisterTask.File = new FileInfo(vb6ArtifactPath);
            comRegisterTask.Execute();
        }

        private void BuildNet(FileInfo component)
        {
            MsbuildTask msbuildTask = CreateTask<MsbuildTask>();
            msbuildTask.ProjectFile = component;
            PropertyTask buildConfigProperty = CreateTask<PropertyTask>();
            buildConfigProperty.PropertyName = "Configuration";
            switch (BuildConfiguration)
            {
                case BuildConfiguration.debug:
                    buildConfigProperty.Value = NET_DEBUG_CONFIGURATION;
                    break;
                case BuildConfiguration.release:
                    buildConfigProperty.Value = NET_RELEASE_CONFIGURATION;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            msbuildTask.Properties.Add(buildConfigProperty);
            msbuildTask.Execute();
        }
    }

    [TypeConverter(typeof(EnumConverter))]
    public enum BuildConfiguration
    {
        debug,
        release
    }
}
