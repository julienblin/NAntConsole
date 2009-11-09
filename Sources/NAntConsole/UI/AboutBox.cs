using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using CDS.Framework.Tools.NAntConsole.Configuration;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    partial class AboutBox : Form
    {
        const string HELP_FILE_INDEX = @"help\index.html";

        public AboutBox()
        {
            InitializeComponent();

            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            this.Text = String.Format(Resources.AboutBoxTitle, AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format(Resources.AboutBoxVersion, AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;

            baseAppDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        string baseAppDirectory;

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public Version AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        #endregion

        private void OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string helFileIndex = Path.Combine(baseAppDirectory, HELP_FILE_INDEX);
            if (File.Exists(helFileIndex))
            {
                Process.Start(helFileIndex);
            }
            else
            {
                MessageBox.Show(this, Resources.HelpNotAvalaible, Resources.ErrorCaption, MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void OnLinkLabelReleaseNotesClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NAntConsoleConfigurationSection configSection = NAntConsoleConfigurationSection.GetConfigurationSection();
            string releaseNoteFile = Path.Combine(baseAppDirectory, configSection.ReleaseNotesPath);
            if (File.Exists(releaseNoteFile))
            {
                Process.Start(releaseNoteFile);
            } else
            {
                MessageBox.Show(this, Resources.ReleaseNotesNotAvalaible, Resources.ErrorCaption, MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }
    }
}
