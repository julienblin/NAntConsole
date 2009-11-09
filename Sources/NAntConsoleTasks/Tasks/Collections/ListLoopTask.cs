using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Collections
{
    [TaskName("list-foreach")]
    public class ListLoopTask : TaskContainer
    {
        private string listName;
        [TaskAttribute("list", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }

        private string valuePropertyName;
        [TaskAttribute("value-property", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string ValuePropertyName
        {
            get { return valuePropertyName; }
            set { valuePropertyName = value; }
        }

        private TaskContainer stuffToDo;

        [BuildElement("do")]
        public TaskContainer StuffToDo
        {
            get{ return stuffToDo; }
            set { stuffToDo = value; }
        }

        protected override void ExecuteTask()
        {
            if(!ListManager.Instance.Lists.ContainsKey(ListName))
            {
                throw new BuildException(string.Format(Resources.ListLoopListNotFound, ListName));    
            }

            IList<string> list = ListManager.Instance.Lists[ListName];
            foreach (string value in list)
            {
                DoWork(value);
            }
        }

        protected virtual void DoWork(string value)
        {
            Properties[ValuePropertyName] = value;
            if (StuffToDo == null)
            {
                base.ExecuteChildTasks();
            }
            else
            {
                StuffToDo.Execute();
            }
        }
    }
}
