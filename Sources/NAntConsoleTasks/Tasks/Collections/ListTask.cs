using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Collections
{
    [TaskName("list")]
    public class ListTask : Task
    {
        private string listName;
        [TaskAttribute("list", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }

        private readonly ArrayList items = new ArrayList();
        [BuildElementArray("item", ElementType = typeof(ListItemElement))]
        public ArrayList Items
        {
            get
            {
                return items;
            }
        }

        protected override void ExecuteTask()
        {
            IList<string> list = null;
            if (ListManager.Instance.Lists.ContainsKey(ListName))
            {
                list = ListManager.Instance.Lists[ListName];
            }
            else
            {
                list = new List<string>();
                ListManager.Instance.Lists.Add(ListName, list);
            }

            foreach (ListItemElement listItemElement in items)
            {
                list.Add(listItemElement.ItemValue);
            }
        }
    }
}
