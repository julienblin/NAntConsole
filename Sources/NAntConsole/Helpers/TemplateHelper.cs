using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Activa.LazyParser;
using Activa.SharpTemplate;
using Activa.SharpTemplate.Config;
using SharpSvn;

namespace CDS.Framework.Tools.NAntConsole.Helpers
{
    public static class TemplateHelper
    {
        private const string TEMPLATE_DIR = @"Templates";

        public static void GenerateTemplateHierarchy(DirectoryInfo targetDir, string sourceDir, IParserContext context, Encoding encoding)
        {
            SharpTemplate template = new SharpTemplate<ProMeshHtml>();

            string templateDir = GetTemplateBaseDir().FullName;
            DirectoryInfo sourceDirInfo = new DirectoryInfo(Path.Combine(templateDir, sourceDir));

            foreach (FileInfo sourcefileInfo in sourceDirInfo.GetFiles())
            {
                if (!sourcefileInfo.Name.Equals("svn-properties.xml", StringComparison.InvariantCulture))
                {
                    string targetFileName = template.Render(sourcefileInfo.Name, context);
                    using (StreamWriter writer = new StreamWriter(Path.Combine(targetDir.FullName, targetFileName), false, encoding))
                    {
                        writer.Write(template.RenderFile(sourcefileInfo.FullName, context));
                    }
                }
                else
                {
                    // Apply svn-properties to parent
                    SvnHelper.AddFolder(targetDir.FullName);
                    XmlDocument propertiesDoc = new XmlDocument();
                    propertiesDoc.Load(sourcefileInfo.FullName);
                    ApplyProperties(targetDir.FullName, propertiesDoc);
                }
            }

            foreach (DirectoryInfo dirInfo in sourceDirInfo.GetDirectories())
            {
                string targetSubDirectoryName = template.Render(dirInfo.Name, context);
                DirectoryInfo targetSubDirectory = new DirectoryInfo(Path.Combine(targetDir.FullName, targetSubDirectoryName));
                if (!targetSubDirectory.Exists)
                {
                    targetSubDirectory.Create();
                }

                GenerateTemplateHierarchy(targetSubDirectory, dirInfo.FullName.Replace(string.Concat(templateDir, @"\"), string.Empty), context, encoding);
            }
        }

        public static void GenerateFile(FileInfo targetFile, string sourceFile, IParserContext context,
                                        Encoding encoding)
        {
            SharpTemplate template = new SharpTemplate<ProMeshHtml>();

            FileInfo sourceFileInfo = new FileInfo(Path.Combine(GetTemplateBaseDir().FullName, sourceFile));

            using (StreamWriter writer = new StreamWriter(targetFile.FullName, false, encoding))
            {
                writer.Write(template.RenderFile(sourceFileInfo.FullName, context));
            }
        }

        private static DirectoryInfo GetTemplateBaseDir()
        {
            return new DirectoryInfo(Path.Combine(Path.GetDirectoryName(typeof(TemplateHelper).Assembly.Location), TEMPLATE_DIR)); 
        }

        private static void ApplyProperties(string directory, XmlDocument doc)
        {
            XmlNodeList propertyList = doc.GetElementsByTagName("property");
            foreach (XmlNode propertyNode in propertyList)
            {
                string propertyText = propertyNode.InnerText;
                if (propertyText.Contains(@"|"))
                {
                    SvnHelper.SetProperty(directory, propertyNode.Attributes["name"].Value, string.Join("\n", propertyText.Split('|')));
                }
                else
                {
                    SvnHelper.SetProperty(directory, propertyNode.Attributes["name"].Value, propertyText);
                }
            }
        }
    }
}
