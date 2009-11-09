using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Functions
{
    [FunctionSet("guid", "guid")]
    public class GuidFunctions : FunctionSetBase
    {
        public GuidFunctions(Project project, PropertyDictionary properties)
            : base(project, properties)
        {
        }

        [Function("generate-guid")]
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
