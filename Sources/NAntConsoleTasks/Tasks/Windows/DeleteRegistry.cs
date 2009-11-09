using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Win32;
using NAnt.Core;
using NAnt.Core.Attributes;
using Registry=Microsoft.Win32.Registry;
using RegistryHive=Microsoft.Win32.RegistryHive;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows
{
    [TaskName("delete-registry")]
    public class DeleteRegistryTask : Task
    {
        #region Private Instance Fields

        private string _regKey = null;
        private string _regKeyValueName = null;
        private RegistryHive[] _regHive = new RegistryHive[] { RegistryHive.LocalMachine };
        private string _regHiveString = RegistryHive.LocalMachine.ToString();

        #endregion Private Instance Fields

        #region Public Instance Properties

        /// <summary>
        /// The registry key to delete, including the path.
        /// </summary>
        /// <remarks>
        /// If the last component of the path is *, the whole subkey is removed.
        /// </remarks>
        /// <example>
        /// SOFTWARE\NAnt-Test
        /// </example>
        [TaskAttribute("key", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public virtual string RegistryKey
        {
            get { return _regKey; }
            set
            {
                string key = value;
                if (value.StartsWith("\\"))
                {
                    key = value.Substring(1);
                }
                string[] pathParts = key.Split("\\".ToCharArray(0, 1)[0]);
                //split the key/path apart.
                _regKeyValueName = pathParts[pathParts.Length - 1];
                _regKey = key.Substring(0, (value.Length - _regKeyValueName.Length));
            }
        }

        /// <summary>
        /// Space separated list of registry hives to search for <see cref="RegistryKey" />.
        /// For a list of possible values, see <see cref="RegistryHive" />. The 
        /// default is <see cref="RegistryHive.LocalMachine" />.
        /// </summary>
        /// <remarks>
        /// <seealso cref="RegistryHive" />
        /// </remarks>
        [TaskAttribute("hive")]
        public virtual string RegistryHiveName
        {
            get { return _regHiveString; }
            set
            {
                _regHiveString = value;
                string[] tempRegHive = _regHiveString.Split(" ".ToCharArray()[0]);
                _regHive = (RegistryHive[])Array.CreateInstance(typeof(RegistryHive), tempRegHive.Length);
                for (int x = 0; x < tempRegHive.Length; x++)
                {
                    _regHive[x] = (RegistryHive)Enum.Parse(typeof(RegistryHive), tempRegHive[x], true);
                }
            }
        }

        #endregion Public Instance Properties

        #region Override implementation of Task

        /// <summary>
        /// Delete the specified value or key from the Registry.
        /// </summary>
        protected override void ExecuteTask()
        {
            if (_regKey == null)
            {
                throw new BuildException("Registry key missing!");
            }

            try
            {
                foreach (RegistryHive hive in _regHive)
                {
                    Microsoft.Win32.RegistryKey regKey = GetHiveKey(hive);

                    if (regKey != null)
                    {
                        if (_regKeyValueName == "*")
                        {
                            // Delete the whole subtree.
                            regKey.DeleteSubKeyTree(_regKey);
                            string infoMessage = string.Format(CultureInfo.InvariantCulture, "{0} deleted.", _regKey);
                            Log(Level.Info, infoMessage);
                        }
                        else
                        {
                            // Delete only the specified value.
                            RegistryKey subKey = regKey.CreateSubKey(_regKey);
                            if (subKey != null)
                            {
                                subKey.DeleteValue(_regKeyValueName, true);
                                string infoMessage = string.Format(CultureInfo.InvariantCulture, "{0}\\{1} deleted.", _regKey, _regKeyValueName);
                                Log(Level.Info, infoMessage);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new BuildException("Deleting registry key or value failed!", e);
            }
        }

        #endregion Override implementation of Task

        #region Protected Instance Methods

        /// <summary>
        /// Returns the key for a given registry hive.
        /// </summary>
        /// <param name="hive">The registry hive to return the key for.</param>
        /// <returns>
        /// The key for a given registry hive.
        /// </returns>
        protected Microsoft.Win32.RegistryKey GetHiveKey(RegistryHive hive)
        {
            switch (hive)
            {
                case RegistryHive.LocalMachine:
                    return Registry.LocalMachine;
                case RegistryHive.Users:
                    return Registry.Users;
                case RegistryHive.CurrentUser:
                    return Registry.CurrentUser;
                case RegistryHive.ClassesRoot:
                    return Registry.ClassesRoot;
                default:
                    Log(Level.Verbose, "Registry not found for {0}.", hive.ToString());
                    return null;
            }
        }

        #endregion Protected Instance Methods
    }
}
