using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.VB
{
    [TaskName("vb6-update-version")]
    public class VB6UpdateVersionTask : Task
    {
        static readonly Regex reMajorVer = new Regex(@"MajorVer=(?<value>[0-9]+)", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);
        static readonly Regex reMinorVer = new Regex(@"MinorVer=(?<value>[0-9]+)", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);
        static readonly Regex reRevisionVer = new Regex(@"RevisionVer=(?<value>[0-9]+)", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);

        private FileInfo vbProject;
        [TaskAttribute("project", Required = true)]
        public FileInfo VbProject
        {
            get { return vbProject; }
            set { vbProject = value; }
        }

        private Version targetVersion;
        [TaskAttribute("version")]
        public Version TargetVersion
        {
            get { return targetVersion; }
            set { targetVersion = value; }
        }

        protected override void ExecuteTask()
        {
            if (!VbProject.Exists)
            {
                throw new BuildException(string.Format(Resources.MissingFile, VbProject.FullName));
            }

            if (TargetVersion == null)
            {
                if (Project.Properties.Contains(EnvIncludeConstants.DEFAULT_VERSION_PROPERTY_NAME))
                {
                    TargetVersion = new Version(Project.Properties[EnvIncludeConstants.DEFAULT_VERSION_PROPERTY_NAME]);
                }
                else
                {
                    throw new BuildException(string.Format(Resources.VB6UpdateVersionMissingVersion, EnvIncludeConstants.DEFAULT_VERSION_PROPERTY_NAME));
                }
            }

            string vbProjContent = File.ReadAllText(VbProject.FullName, Encoding.GetEncoding(@"ISO-8859-1"));
            Version currentVersion = GetCurrentVersion(vbProjContent);

            if ((currentVersion.Major != TargetVersion.Major) || (currentVersion.Minor != TargetVersion.Minor) ||
                (currentVersion.Build != TargetVersion.Build))
            {
                Log(Level.Info, string.Format(Resources.VB6UpdateVersionUpdating, VbProject.Name,
                                string.Concat(TargetVersion.Major, ".", TargetVersion.Minor, ".", TargetVersion.Build)));

                vbProjContent = reMajorVer.Replace(vbProjContent, string.Concat(@"MajorVer=", TargetVersion.Major), 1);
                vbProjContent = reMinorVer.Replace(vbProjContent, string.Concat(@"MinorVer=", TargetVersion.Minor), 1);
                vbProjContent = reRevisionVer.Replace(vbProjContent, string.Concat(@"RevisionVer=", TargetVersion.Build), 1);

                using (StreamWriter writer = new StreamWriter(VbProject.FullName, false, Encoding.GetEncoding(@"ISO-8859-1")))
                {
                    writer.Write(vbProjContent);
                }
            }
        }

        private static Version GetCurrentVersion(string content)
        {
            int major = 1;
            int minor = 0;
            int build = 0;
            using (StringReader reader = new StringReader(content))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    Match mMajorVer = reMajorVer.Match(line);
                    if (mMajorVer.Success)
                    {
                        major = Convert.ToInt32(mMajorVer.Groups["value"].Value);
                    }

                    Match mMinorVer = reMinorVer.Match(line);
                    if (mMinorVer.Success)
                    {
                        minor = Convert.ToInt32(mMinorVer.Groups["value"].Value);
                    }

                    Match mRevisionVer = reRevisionVer.Match(line);
                    if (mRevisionVer.Success)
                    {
                        build = Convert.ToInt32(mRevisionVer.Groups["value"].Value);
                    }
                    line = reader.ReadLine();
                }
            }
            return new Version(major, minor, build);
        }
    }
}
