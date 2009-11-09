using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks
{
    [TaskName("env-include")]
    public class EnvIncludeTask : Task
    {
        
        static readonly Regex reFileNameSelector = new Regex(@"(?<projectName>[^\.]+)\.(?<selector>[^\.]+)\.config", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        protected override void ExecuteTask()
        {

            DirectoryInfo envDirInfo = new DirectoryInfo(Path.Combine(Project.BaseDirectory, EnvIncludeConstants.ENV_FOLDER_NAME));

            if (!envDirInfo.Exists)
            {
                Log(Level.Info, Resources.EnvIncludeMissingEnvDir, envDirInfo.FullName);
                return;
            }

            FileInfo[] configFiles = envDirInfo.GetFiles("*.config", SearchOption.AllDirectories);
            Dictionary<string, Dictionary<string, FileInfo>> configFilesByMiddle = SortConfigFiles(configFiles);

            List<string> projectAdded = new List<string>();

            IncludeProjects(configFilesByMiddle, projectAdded, Environment.MachineName);

            string envVarValue = Environment.GetEnvironmentVariable(EnvIncludeConstants.ENV_VAR_NAME);
            if (!string.IsNullOrEmpty(envVarValue))
            {
                IncludeProjects(configFilesByMiddle, projectAdded, envVarValue);
            }

            IncludeProjects(configFilesByMiddle, projectAdded, EnvIncludeConstants.DEFAULT_CONFIG);

            if (projectAdded.Count == 0)
            {
                Log(Level.Info, Resources.EnvIncludeNoInclude);
            }
        }

        private void IncludeProjects(IDictionary<string, Dictionary<string, FileInfo>> configFilesByMiddle, ICollection<string> projectAdded, string selectorValue)
        {
            string suitableValue = FindSuitableValue(configFilesByMiddle, selectorValue);
            if (configFilesByMiddle.ContainsKey(suitableValue))
            {
                Dictionary<string, FileInfo> projects = configFilesByMiddle[suitableValue];
                foreach (string projectName in projects.Keys)
                {
                    if(!projectAdded.Contains(projectName))
                    {
                        IncludeTask includeTask = new IncludeTask();
                        CopyTo(includeTask);
                        includeTask.BuildFileName = projects[projectName].FullName;
                        includeTask.Execute();
                        projectAdded.Add(projectName);
                    }
                }
            }
        }

        private string FindSuitableValue(IDictionary<string, Dictionary<string, FileInfo>> configFilesByMiddle, string value)
        {
            foreach (string selectorName in configFilesByMiddle.Keys)
            {
                if (selectorName.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                {
                    return selectorName;
                }
            }
            return value;
        }

        private static Dictionary<string, Dictionary<string, FileInfo>> SortConfigFiles(IEnumerable<FileInfo> configFiles)
        {
            Dictionary<string, Dictionary<string, FileInfo>> result = new Dictionary<string, Dictionary<string, FileInfo>>();

            foreach (FileInfo configFile in configFiles)
            {
                Match mSelector = reFileNameSelector.Match(configFile.Name);
                if (mSelector.Success)
                {
                    string selector = mSelector.Groups["selector"].Value;
                    string projectName = mSelector.Groups["projectName"].Value;
                    if (!result.ContainsKey(selector))
                    {
                        result.Add(selector, new Dictionary<string, FileInfo>());
                    }

                    if (!result[selector].ContainsKey(projectName))
                    {
                        result[selector].Add(projectName, configFile);
                    }
                    else
                    {
                        Debug.Assert(false);
                    }
                }
            }

            return result;
        }
    }
}
