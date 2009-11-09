using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using CDS.Framework.Tools.NAntConsole.Configuration;
using CDS.Framework.Tools.NAntConsole.Entities;

namespace CDS.Framework.Tools.NAntConsole.Helpers
{
    public static class NAntHelper
    {
        private const string NANT_NAMESPACE = @"http://nant.sf.net/schemas/nant.xsd";
        private const string NANT_EXE = @"NAnt.exe";
        private const string NANTCONSOLE_VERSION_PROPERTY_NAME = @"nantconsole.version";

        public static int ExecuteNant(NAntProject nantProject, string targetName, OnNAntExecutionProgress output)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string nantExePath = Path.Combine(Path.GetDirectoryName(currentAssembly.Location), NANT_EXE);
            ProcessStartInfo processStartInfo = new ProcessStartInfo(nantExePath);
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.WorkingDirectory = Path.GetDirectoryName(currentAssembly.Location);

            processStartInfo.Arguments = GetNAntArgs(nantProject, targetName);
            processStartInfo.CreateNoWindow = true;

            int exitCode = Int32.MinValue;
            using (Process proc = new Process())
            {
                proc.StartInfo = processStartInfo;

                proc.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
                                               {
                                                   if (!string.IsNullOrEmpty(e.Data))
                                                   {
                                                       output(new NAntExecutionProgressEventArgs(e.Data));
                                                   }
                                                   else
                                                   {
                                                       output(new NAntExecutionProgressEventArgs(Environment.NewLine));
                                                   }
                                               };

                proc.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
                                              {
                                                  if (!string.IsNullOrEmpty(e.Data))
                                                  {
                                                      output(new NAntExecutionProgressEventArgs(e.Data));
                                                  }
                                                  else
                                                  {
                                                      output(new NAntExecutionProgressEventArgs(Environment.NewLine));
                                                  }
                                              };

                proc.Start();
                proc.BeginOutputReadLine();
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }

            return exitCode;
        }

        private static string GetNAntArgs(NAntProject nantProject, string targetName)
        {
            StringBuilder sbArgs = new StringBuilder();
            sbArgs.AppendFormat("-nologo -buildfile:\"{0}\" \"{1}\"", nantProject.BuildFile.FullName, targetName);
            NAntConsoleConfigurationSection config = NAntConsoleConfigurationSection.GetConfigurationSection();
            foreach (NAntPropertyElement nAntProperty in config.NAntProperties)
            {
                sbArgs.AppendFormat(" -D:\"{0}\"=\"{1}\"", nAntProperty.Name, nAntProperty.Value);
            }
            sbArgs.AppendFormat(" -D:\"{0}\"=\"{1}\"", NANTCONSOLE_VERSION_PROPERTY_NAME, Assembly.GetAssembly(typeof(NAntHelper)).GetName().Version);

            return sbArgs.ToString();
        }

        public static NAntProject LoadProject(FileInfo nantProjectBuildFile)
        {
            NAntProject nantProject = new NAntProject(nantProjectBuildFile);
            XmlDocument buildDoc = new XmlDocument();
            buildDoc.Load(nantProjectBuildFile.FullName);
            
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(buildDoc.NameTable);
            namespaceManager.AddNamespace("nant", NANT_NAMESPACE);

            LoadProjectProperties(nantProject, buildDoc, namespaceManager);
            LoadTargets(nantProject, buildDoc, namespaceManager);
            return nantProject;
        }

        private static void LoadProjectProperties(NAntProject project, XmlDocument buildDoc, XmlNamespaceManager namespaceManager)
        {
            XmlNode projectNode = buildDoc.SelectSingleNode(@"/nant:project", namespaceManager);
            if (projectNode != null)
            {
                project.ProjectName = GetAttributeValue(projectNode, @"name");
                project.DefaultTargetName = GetAttributeValue(projectNode, @"default");
            }
        }

        private static void LoadTargets(NAntProject project, XmlDocument buildDoc, XmlNamespaceManager namespaceManager)
        {
            XmlNodeList targetNodes = buildDoc.SelectNodes(@"//nant:target", namespaceManager);
            if (targetNodes != null)
            {
                foreach (XmlNode targetNode in targetNodes)
                {
                    NAntTarget target = new NAntTarget(project, GetAttributeValue(targetNode, @"name"));
                    target.Description = GetAttributeValue(targetNode, @"description");
                    string dependeciesList = GetAttributeValue(targetNode, @"depends");
                    if (!string.IsNullOrEmpty(dependeciesList))
                    {
                        string[] splittedDependencies = dependeciesList.Split(',');
                        foreach (string dependency in splittedDependencies)
                        {
                            target.Dependencies.Add(dependency.Trim());
                        }
                    }
                    project.Targets.Add(target);
                }
            }
        }

        private static string GetAttributeValue(XmlNode node, string attrName)
        {
            return node.Attributes[attrName] != null ? node.Attributes[attrName].Value : string.Empty;
        }

        public delegate void OnNAntExecutionProgress(NAntExecutionProgressEventArgs args);
    }
}
