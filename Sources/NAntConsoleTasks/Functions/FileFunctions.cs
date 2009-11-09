using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Functions
{
    [FunctionSet("file", "file")]
    public class FileFunctions : FunctionSetBase
    {
        public FileFunctions(Project project, PropertyDictionary properties)
            : base(project, properties)
        {
        }

        [Function("are-the-same")]
        public static bool AreTheSame(string file1Path, string file2Path)
        {
            try
            {
                using (FileStream fs1 = new FileStream(file1Path, FileMode.Open))
                {
                    using (FileStream fs2 = new FileStream(file2Path, FileMode.Open))
                    {
                        if (fs1.Length != fs2.Length)
                        {
                            return false;
                        }

                        int file1Byte;
                        int file2Byte;
                        do
                        {
                            file1Byte = fs1.ReadByte();
                            file2Byte = fs2.ReadByte();
                        } while ((file1Byte == file2Byte) && (file1Byte != -1));

                        return ((file1Byte - file2Byte) == 0);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
