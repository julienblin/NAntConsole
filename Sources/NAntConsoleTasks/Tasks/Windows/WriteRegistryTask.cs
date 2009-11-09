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
    [TaskName("write-registry")]
    public class WriteRegistryTask : Task
    {
        #region Private Instance Fields

        private string _regKeyValue = null;
        private string _regKey = null;
        private string _regKeyValueName = null;
        private RegistryHive[] _regHive = new RegistryHive[] { RegistryHive.LocalMachine };
        private string _regHiveString = RegistryHive.LocalMachine.ToString();

        #endregion

        #region Public Instance Properties

        /// <summary>
        /// Value to be stored in the Registry.
        /// </summary>
        [TaskAttribute("value", Required = true)]
        public virtual string RegistryKeyValue
        {
            get { return _regKeyValue; }
            set { _regKeyValue = value; }
        }

        /// <summary>
        /// The registry key to write to, including the path.
        /// </summary>
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

        private RegistryValueKind valueKind = RegistryValueKind.Unknown;
        [TaskAttribute("kind")]
        public RegistryValueKind ValueKind
        {
            get { return valueKind; }
            set { valueKind = value; }
        }

        #endregion Public Instance Properties

        #region Override implementation of Task

        /// <summary>
        /// Write the specified value to the Registry.
        /// </summary>
        protected override void ExecuteTask()
        {
            if (_regKey == null)
            {
                throw new BuildException("Missing registry key!");
            }

            if (_regKeyValue == null)
            {
                throw new BuildException("Missing value!");
            }

            try
            {
                foreach (RegistryHive hive in _regHive)
                {
                    RegistryKey regKey = GetHiveKey(hive);

                    if (regKey != null)
                    {
                        RegistryKey newKey = regKey.CreateSubKey(_regKey);

                        if (newKey != null)
                        {
                            if (valueKind == RegistryValueKind.DWord)
                            {
                                newKey.SetValue(_regKeyValueName, Convert.ToInt32(_regKeyValue), valueKind);
                            }
                            else
                            {
                                newKey.SetValue(_regKeyValueName, _regKeyValue, valueKind);
                            }
                            string infoMessage = string.Format(CultureInfo.InvariantCulture, "{0}{1} set to {2}.", _regKey, _regKeyValueName, _regKeyValue);
                            Log(Level.Info, infoMessage);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new BuildException("Writing to registry failed!", e);
            }
        }

        #endregion

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
