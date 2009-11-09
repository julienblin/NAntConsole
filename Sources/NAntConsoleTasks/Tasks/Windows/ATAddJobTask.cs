using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;
using TaskScheduler;
using Task=TaskScheduler.Task;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows
{
    [TaskName("at-add-job")]
    public class ATAddJobTask : BaseATTask
    {
        private FileInfo program;
        [TaskAttribute("program", Required = true)]
        public FileInfo Program
        {
            get { return program; }
            set { program = value; }
        }

        private string arguments;
        [TaskAttribute("args")]
        [StringValidator(AllowEmpty = false)]
        public string Arguments
        {
            get { return arguments; }
            set { arguments = value; }
        }

        private string comment;
        [TaskAttribute("comment", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private Credential credentials;
        [BuildElement("credentials")]
        public Credential Credentials
        {
            get { return credentials; }
            set { credentials = value; }
        }

        private DirectoryInfo workingDirectory;
        [TaskAttribute("workingDirectory")]
        public DirectoryInfo WorkingDirectory
        {
            get { return workingDirectory; }
            set { workingDirectory = value; }
        }

        private ATTriggersCollectionElement triggers;
        [BuildElement("triggers", Required = true)]
        public ATTriggersCollectionElement Triggers
        {
            get { return triggers; }
            set { triggers = value; }
        }


        protected override void ExecuteATTask(ScheduledTasks scheduledTasks)
        {
            ATDelJobTask delJobTask = new ATDelJobTask();
            CopyTo(delJobTask);
            delJobTask.Parent = this;
            delJobTask.TaskName = TaskName;
            delJobTask.Execute();

            Log(Level.Info, Resources.ATAddJobAdding, TaskName);
            using (Task newTask = scheduledTasks.CreateTask(TaskName))
            {
                newTask.ApplicationName = Program.FullName;
                newTask.Comment = Comment;
                if (!string.IsNullOrEmpty(Arguments))
                    newTask.Parameters = Arguments;

                if (Credentials != null)
                {
                    newTask.SetAccountInformation(string.Concat(Credentials.Domain, @"\", Credentials.UserName), Credentials.Password);
                }

                if (WorkingDirectory != null)
                    newTask.WorkingDirectory = WorkingDirectory.FullName;

                newTask.Creator = string.Format(@"[NAntConsole] {0}\{1}", Environment.UserDomainName, Environment.UserName);

                IList<ATTriggerElement> sheduledTriggers = Triggers.GetTriggers();
                foreach (ATTriggerElement trigger in sheduledTriggers)
                {
                    Log(Level.Info, Resources.ATAddJobAddingTrigger, trigger);
                    newTask.Triggers.Add(trigger.CreateTrigger());
                }

                newTask.Save();
            }
        }
    }
}
