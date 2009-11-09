using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CDS.Framework.Tools.NAntConsoleCmdLine
{
    class Program
    {
        static int Main(string[] args)
        {
            return CommandLineExecution.Execute(new FileInfo(args[0]), args[1]);
        }
    }
}
