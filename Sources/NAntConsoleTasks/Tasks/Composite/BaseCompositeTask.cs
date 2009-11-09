using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite
{
    public abstract class BaseCompositeTask : Task
    {
        protected T CreateTask<T>() where T : Task, new()
        {
            T task = new T();
            CopyTo(task);
            task.Parent = this;
            return task;
        }
    }
}
