using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class COMRoleElement : Element
    {
        private string roleName;
        [TaskAttribute("name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        private readonly ArrayList users = new ArrayList();
        [BuildElementArray("user", ElementType = typeof(COMRoleUserElement))]
        public ArrayList Users
        {
            get
            {
                return users;
            }
        }
    }
}
