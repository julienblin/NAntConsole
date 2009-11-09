using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CDS.Framework.Tools.NAntConsole.Configuration
{
    public class NAntConsoleConfigurationSection : ConfigurationSection
    {
        private const string CONFIGURATION_SECTION = @"nantconsole";
        public const string NANTCONSOLE_REGKEY = @"Software\NAntConsole";
        private const string NANTCONSOLE_CHECKOUT_DIRECTORY_REGKEY = @"CheckOutDirectory";

        private static NAntConsoleConfigurationSection loadedConfigurationSection;

        public static NAntConsoleConfigurationSection GetConfigurationSection()
        {
            if (loadedConfigurationSection == null)
            {
                loadedConfigurationSection = (NAntConsoleConfigurationSection)ConfigurationManager.GetSection(CONFIGURATION_SECTION);
            }
            return loadedConfigurationSection;
        }

        [ConfigurationProperty("svn-repositories")]
        [ConfigurationCollection(typeof(SvnRepositoriesCollection), AddItemName = "repository")]
        public SvnRepositoriesCollection SvnRepositories
        {
            get
            {
                return (SvnRepositoriesCollection)base["svn-repositories"];
            }
        }

        [ConfigurationProperty("nant-properties", IsRequired = true)]
        [ConfigurationCollection(typeof(NAntPropertiesCollection), AddItemName = "property")]
        public NAntPropertiesCollection NAntProperties
        {
            get
            {
                return (NAntPropertiesCollection)base["nant-properties"];
            }
        }

        [ConfigurationProperty("releaseNotes")]
        public string ReleaseNotesPath
        {
            get
            {
                return (string)this["releaseNotes"];
            }
            set
            {
                this["releaseNotes"] = value;
            }
        }

        [ConfigurationProperty("timespanBetweenCheckForUpdatesInMinutes")]
        public int TimespanBetweenCheckForUpdatesInMinutes
        {
            get
            {
                return (int)this["timespanBetweenCheckForUpdatesInMinutes"];
            }
            set
            {
                this["timespanBetweenCheckForUpdatesInMinutes"] = value;
            }
        }

        [ConfigurationProperty("updater")]
        public UpdaterElement Updater
        {
            get
            {
                return (UpdaterElement)this["updater"];
            }
            set
            {
                this["updater"] = value;
            }
        }

        private static string checkOutDirectory;

        public static string GetCheckOutDirectory()
        {
            if (string.IsNullOrEmpty(checkOutDirectory))
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(NANTCONSOLE_REGKEY, RegistryKeyPermissionCheck.ReadSubTree);
                if (key != null)
                {
                    checkOutDirectory = (string)key.GetValue(NANTCONSOLE_CHECKOUT_DIRECTORY_REGKEY);
                }

                if (string.IsNullOrEmpty(checkOutDirectory))
                {
                    AskCheckOutDirectory();
                }
            }
            return checkOutDirectory;
        }

        public static void AskCheckOutDirectory()
        {
            MessageBox.Show(Resources.SelectCheckOutDirectory, string.Empty, MessageBoxButtons.OK,
                                    MessageBoxIcon.Asterisk);
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            DialogResult lastResult = folderBrowserDialog.ShowDialog();
            while (lastResult != DialogResult.OK)
            {
                lastResult = folderBrowserDialog.ShowDialog();
            }
            checkOutDirectory = folderBrowserDialog.SelectedPath;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(NANTCONSOLE_REGKEY, RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(NANTCONSOLE_REGKEY, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
            key.SetValue(NANTCONSOLE_CHECKOUT_DIRECTORY_REGKEY, checkOutDirectory, RegistryValueKind.String);
        }
    }
}