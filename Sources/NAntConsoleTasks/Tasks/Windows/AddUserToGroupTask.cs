using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.DirectoryServices;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows
{
    [TaskName("add-usertogroup")]
    public class AddUserToGroupTask : Task
    {

        private string userName;
        [TaskAttribute("userName", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string groupName;
        [TaskAttribute("groupName", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        private string machineName;
        [TaskAttribute("machineName", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string MachineName
        {
            get { return machineName; }
            set { machineName = value; }
        }

        private string domainName;
        [TaskAttribute("domainName", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string DomainName
        {
            get { return domainName; }
            set { domainName = value; }
        }

        protected override void ExecuteTask()
        {
            
            AddUserToLocalGroup(UserName, GroupName, DomainName, MachineName);
        }

        protected bool AddUserToLocalGroup(string user, string groupName, string domainName, string machine)
        {
            bool reponse = false;

            try
            {
                string userPath = string.Format("WinNT://{0}/{1},user", domainName, user);
                string groupPath = string.Format("WinNT://{0}/{1},group", machine, groupName);

                using (DirectoryEntry groupe = new DirectoryEntry(groupPath))
                {

                    groupe.Invoke("Add", userPath);
                    groupe.CommitChanges();
                    groupe.Close();

                }
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                Log(Level.Error, E.Message.ToString());
            }

            return reponse;
        }
    }
}
