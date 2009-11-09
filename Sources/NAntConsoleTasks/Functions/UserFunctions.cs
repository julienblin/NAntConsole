using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core.Attributes;
using System.DirectoryServices;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.IIS;
using NAnt.Core;
using System.Diagnostics;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Functions
{
    [FunctionSet("user", "user")]
    public class UserFunctions : FunctionSetBase
    {
        public UserFunctions(Project project, PropertyDictionary properties)
            : base(project, properties)
        {
        }

        [Function("get-nt-account")]
        public static string GetNtAccount()
        {
            return Environment.UserDomainName + @"\" + Environment.UserName;
        }
    }
}
