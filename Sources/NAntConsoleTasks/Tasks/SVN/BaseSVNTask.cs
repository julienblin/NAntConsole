using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using SharpSvn;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.SVN
{
    public abstract class BaseSVNTask : Task
    {
        protected override void ExecuteTask()
        {
            using(SvnClient svnClient = new SvnClient())
            {
                ExecuteSVNTask(svnClient);
            }
        }

        protected abstract void ExecuteSVNTask(SvnClient client);
    }
}
