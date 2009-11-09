using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using CDS.Framework.Tools.NAntConsole.Helpers;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite;
using ICSharpCode.SharpZipLib.Zip;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public partial class ViewInstall : Form
    {
        private const string NANT_XSL_PATH = @"UI\xsl\Nant-html.xsl";
        private const string NANT_TASKS_HELP_PATH = @"Help\tasks";

        public ViewInstall()
        {
            InitializeComponent();
            ConnectEventHandlers();
        }

        private FileInfo deployFile;

        public FileInfo DeployFile
        {
            get { return deployFile; }
            set { deployFile = value; }
        }

        private FileInfo htmlFile;

        private string baseAppDirectory;

        private void ConnectEventHandlers()
        {
            buttonOk.Click += OnButtonOkClick;
        }

        protected override void OnLoad(EventArgs e)
        {
            baseAppDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ExtractNAntFile();
            if (htmlFile != null)
            {
                webBrowserViewInstall.Url = new Uri(string.Format("file://{0}", htmlFile.FullName));
                webBrowserViewInstall.Navigating += OnWebBrowserViewInstallNavigating;
                webBrowserViewInstall.Focus();
            }
            else
            {
                Close();
            }
            base.OnLoad(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                if (htmlFile != null)
                {
                    htmlFile.Delete();
                }
            }
            catch
            { }
            base.OnFormClosed(e);
        }

        private void OnButtonOkClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnWebBrowserViewInstallNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {

            e.Cancel = true;
            string taskHtmlFileName = Path.GetFileName(e.Url.LocalPath);
            string fullTaskHtmlFileName = Path.Combine(baseAppDirectory,
                                                        string.Concat(NANT_TASKS_HELP_PATH, @"\", taskHtmlFileName));
            if (File.Exists(fullTaskHtmlFileName))
            {
                Process.Start(fullTaskHtmlFileName);
            }
            else
            {
                MessageBox.Show(this,
                                string.Format(Resources.HelpUnavailable, taskHtmlFileName.Replace(".html", string.Empty)),
                                Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ExtractNAntFile()
        {
            if (!DeployFile.Exists)
            {
                Close();
            }
            else
            {
                DirectoryInfo packageDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
                if (packageDir.Exists)
                {
                    packageDir.Delete(true);
                }

                try
                {
                    string nantFilename = null;
                    ZipHelper.CheckNAntConsoleVersion(DeployFile);
                    ZipHelper.UnZipFilter(DeployFile, packageDir, delegate(ZipEntry entry)
                                  {
                                      if (entry.IsFile)
                                      {
                                          if (entry.Name.Equals(CompositeConstants.DEPLOY_FILE_NAME, StringComparison.InvariantCultureIgnoreCase))
                                          {
                                              nantFilename = entry.Name;
                                              return true;
                                          }
                                      }
                                      return false;
                                  });

                    if (nantFilename == null)
                    {
                        Close();
                    }
                    else
                    {
                        FileInfo nantFile = new FileInfo(Path.Combine(packageDir.FullName, nantFilename));
                        TransformNantFile(nantFile);
                    }

                }
                catch (VersionNotFoundException versionEx)
                {
                    MessageBox.Show(this, versionEx.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, string.Format(Resources.ExecutionError, ex), Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (packageDir.Exists)
                    {
                        packageDir.Delete(true);
                    }
                }
            }
        }

        private void TransformNantFile(FileInfo nantFile)
        {
            string tempXmlNantFile = string.Concat(Path.GetTempFileName(), ".xml");
            nantFile.MoveTo(tempXmlNantFile);
            string resultHtmlFile = string.Concat(Path.GetTempFileName(), ".html");
            try
            {
                XPathDocument myXPathDocument = new XPathDocument(tempXmlNantFile);
                XslCompiledTransform myXslTransform = new XslCompiledTransform();
                using (XmlTextWriter writer = new XmlTextWriter(resultHtmlFile, Encoding.UTF8))
                {
                    myXslTransform.Load(Path.Combine(baseAppDirectory, NANT_XSL_PATH));
                    myXslTransform.Transform(myXPathDocument, null, writer);
                }
                htmlFile = new FileInfo(resultHtmlFile);
            }
            finally
            {
                File.Delete(tempXmlNantFile);
            }
        }
    }
}