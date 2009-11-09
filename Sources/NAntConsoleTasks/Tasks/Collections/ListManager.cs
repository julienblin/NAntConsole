using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Collections
{
    internal sealed class ListManager
    {
        #region Singleton

        private static readonly ListManager instance = new ListManager();

        public static ListManager Instance
        {
            get { return instance; }
        }

        private ListManager()
        {
        }
        #endregion

        private readonly Dictionary<string, IList<string>> lists = new Dictionary<string, IList<string>>();

        public IDictionary<string, IList<string>> Lists
        {
            get { return lists; }
        }
    }
}
