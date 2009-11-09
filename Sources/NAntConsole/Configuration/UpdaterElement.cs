using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;
using Microsoft.Win32;

namespace CDS.Framework.Tools.NAntConsole.Configuration
{
    public class UpdaterElement : ConfigurationElement
    {
        private const string NANTCONSOLE_LAST_UPDATE_CHECK_REGKEY = @"LastUpdateCheck";

        [ConfigurationProperty("productCode", IsRequired = true)]
        public string ProductCode
        {
            get
            {
                return (string)this["productCode"];
            }
            set
            {
                this["productCode"] = value;
            }
        }

        [ConfigurationProperty("timespanBetweenUpdatesInMinutes", IsRequired = true)]
        public int TimespanBetweenUpdatesInMinutes
        {
            get
            {
                return (int)this["timespanBetweenUpdatesInMinutes"];
            }
            set
            {
                this["timespanBetweenUpdatesInMinutes"] = value;
            }
        }

        [ConfigurationProperty("locations", IsRequired = true)]
        [ConfigurationCollection(typeof(UpdaterLocationsCollection), AddItemName = "location")]
        public UpdaterLocationsCollection Locations
        {
            get
            {
                return (UpdaterLocationsCollection)base["locations"];
            }
        }

        public DateTime GetLastUpdateCheckTime()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(NAntConsoleConfigurationSection.NANTCONSOLE_REGKEY, RegistryKeyPermissionCheck.ReadSubTree);
            if (key != null)
            {
                string strLastUpdate = (string)key.GetValue(NANTCONSOLE_LAST_UPDATE_CHECK_REGKEY);
                if (!string.IsNullOrEmpty(strLastUpdate))
                {
                    return DateTime.Parse(strLastUpdate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                }
            }
            return DateTime.MinValue;
        }

        public void SetLastUpdateCheckTime(DateTime dateTime)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(NAntConsoleConfigurationSection.NANTCONSOLE_REGKEY, RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(NAntConsoleConfigurationSection.NANTCONSOLE_REGKEY, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
            key.SetValue(NANTCONSOLE_LAST_UPDATE_CHECK_REGKEY, dateTime.ToString(CultureInfo.InvariantCulture), RegistryValueKind.String);
        }
    }
}
