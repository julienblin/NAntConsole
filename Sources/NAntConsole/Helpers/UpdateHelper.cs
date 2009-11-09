using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CDS.Framework.Tools.NAntConsole.Configuration;

namespace CDS.Framework.Tools.NAntConsole.Helpers
{
    internal static class UpdateHelper
    {
        private const string EVENT_LOG_SOURCE = @"NAntConsole - Updates";
        private const string APPLICATION_LOG = @"Applications";
        private const string MSI_FILE_TEMPLATE_NAME = @"NAntConsole-{0}.msi";
        private const string NANTCONSOLE_UPDATER_EXE = @"CDS.Framework.Tools.NAntConsoleUpdater.exe";

        readonly static Regex reVersion = new Regex(@"^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static UpdateInfo CheckNewVersionAvailability()
        {
            return CheckNewVersionAvailability(false);
        }

        public static UpdateInfo CheckNewVersionAvailability(bool forceCheck)
        {
            try
            {
                NAntConsoleConfigurationSection configurationSection =
                NAntConsoleConfigurationSection.GetConfigurationSection();

                if (!forceCheck)
                {
                    DateTime lastUpdate = configurationSection.Updater.GetLastUpdateCheckTime();
                    DateTime nextPlannedUpdate =
                        lastUpdate.AddMinutes(configurationSection.Updater.TimespanBetweenUpdatesInMinutes);
                    if (nextPlannedUpdate > DateTime.Now)
                    {
                        return null;
                    }
                }

                configurationSection.Updater.SetLastUpdateCheckTime(DateTime.Now);

                Version currentVersion = typeof (UpdateHelper).Assembly.GetName().Version;
                Version bestVersion = new Version(1, 0, 0, 0);
                DirectoryInfo bestVersionDir = null;

                foreach (UpdaterLocation location in configurationSection.Updater.Locations)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(location.Path);
                    if (dirInfo.Exists)
                    {
                        DirectoryInfo[] childDirs = dirInfo.GetDirectories();
                        foreach (DirectoryInfo childDir in childDirs)
                        {
                            if (reVersion.Match(childDir.Name).Success)
                            {
                                Version dirVersion = new Version(childDir.Name);
                                if (dirVersion > currentVersion)
                                {
                                    if(dirVersion > bestVersion)
                                    {
                                        bestVersion = dirVersion;
                                        bestVersionDir = childDir;
                                    }
                                }
                            }
                        }

                        if (bestVersionDir != null)
                        {
                            FileInfo updateFile = new FileInfo(Path.Combine(bestVersionDir.FullName, string.Format(MSI_FILE_TEMPLATE_NAME, bestVersion)));
                            if (updateFile.Exists)
                            {
                                return new UpdateInfo(bestVersion, updateFile, dirInfo);
                            }
                        }

                        // Stop the search when the first location is responsive.
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                try
                {
                    if (EventLog.SourceExists(EVENT_LOG_SOURCE))
                    {
                        EventLog.DeleteEventSource(EVENT_LOG_SOURCE);
                    }
                    EventLog.CreateEventSource(EVENT_LOG_SOURCE, APPLICATION_LOG);
                    string message = string.Format("Error while trying to check updates : {0}", ex);
                    EventLog.WriteEntry(EVENT_LOG_SOURCE, message, EventLogEntryType.Error, 0);
                }
                catch
                {
                }
                return null;
            }
        }

        public static void Update(UpdateInfo info)
        {
            NAntConsoleConfigurationSection configurationSection = NAntConsoleConfigurationSection.GetConfigurationSection();
            string currentDir = Path.GetDirectoryName(typeof (UpdateHelper).Assembly.Location);
            FileInfo nantUpdaterFileInfo = new FileInfo(Path.Combine(currentDir, NANTCONSOLE_UPDATER_EXE));
            nantUpdaterFileInfo = nantUpdaterFileInfo.CopyTo(Path.Combine(Path.GetTempPath(), nantUpdaterFileInfo.Name), true);
            
            Process.Start(nantUpdaterFileInfo.FullName, string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\"", configurationSection.Updater.ProductCode, info.FileInfo, currentDir, info.Version));
        }

        public static void CleanUpArtifactsOnRelaunch()
        {
            FileInfo nantUpdaterFileInfo = new FileInfo(Path.Combine(Path.GetTempPath(), NANTCONSOLE_UPDATER_EXE));
            if (nantUpdaterFileInfo.Exists)
            {
                try
                {
                    nantUpdaterFileInfo.Delete();
                }
                catch
                {
                }
            }
        }
    }

    internal class UpdateInfo
    {
        private readonly Version version;
        private readonly FileInfo fileInfo;
        private readonly DirectoryInfo location;

        public UpdateInfo(Version version, FileInfo fileInfo, DirectoryInfo location)
        {
            this.version = version;
            this.fileInfo = fileInfo;
            this.location = location;
        }

        public DirectoryInfo Location
        {
            get { return location; }
        }

        public FileInfo FileInfo
        {
            get { return fileInfo; }
        }

        public Version Version
        {
            get { return version; }
        }
    }
}
