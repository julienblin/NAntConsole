using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite;
using CDS.Framework.Tools.NAntConsole.UI;
using ICSharpCode.SharpZipLib.Zip;

namespace CDS.Framework.Tools.NAntConsole.Helpers
{
    public static class ZipHelper
    {
        public static void UnZip(FileInfo zipFileName, DirectoryInfo targetDir)
        {
            UnZipFilter(zipFileName, targetDir, delegate { return true; });
        }

        public static void UnZipFilter(FileInfo zipFileName, DirectoryInfo targetDir, Predicate<ZipEntry> filter)
        {
            using (ZipInputStream stream = new ZipInputStream(zipFileName.OpenRead()))
            {
                ZipEntry entry;
                while ((entry = stream.GetNextEntry()) != null)
                {
                    if (filter(entry))
                    {
                        if (entry.IsDirectory)
                        {
                            ExtractDirectory(targetDir, stream, entry.Name, entry.DateTime);
                        }
                        else
                        {
                            ExtractFile(targetDir, stream, entry.Name, entry.DateTime, entry.Size);
                        }
                    }
                }
            }
        }

        public static void CheckNAntConsoleVersion(FileInfo deployPackage)
        {
            ReadInMemory(deployPackage, 
                delegate(ZipEntry entry)
                    {
                        return entry.IsFile && entry.Name.Equals(CompositeConstants.NANTCONSOLE_VERSION_FILE_NAME, StringComparison.InvariantCultureIgnoreCase);
                    },
                 delegate(MemoryStream stream)
                     {
                         using (StreamReader reader = new StreamReader(stream))
                         {
                             Version version = new Version(reader.ReadLine());
                             if (version.CompareTo(typeof(ZipHelper).Assembly.GetName().Version) == 1)
                             {
                                 throw new VersionNotFoundException(string.Format(Resources.VersionError, typeof(ZipHelper).Assembly.GetName().Version, version));
                             }
                         }
                     });
        }

        public static void ReadInMemory(FileInfo zipFileName, Predicate<ZipEntry> filter, Action<MemoryStream> action)
        {
            using (ZipInputStream inputStream = new ZipInputStream(zipFileName.OpenRead()))
            {
                ZipEntry entry;
                while ((entry = inputStream.GetNextEntry()) != null)
                {
                    if (filter(entry))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            int count = 0x800;
                            byte[] buffer = new byte[0x800];
                            if (entry.Size <= 0L)
                            {
                                goto Label_0138;
                            }
                        Label_0116:
                            count = inputStream.Read(buffer, 0, buffer.Length);
                            if (count > 0)
                            {
                                stream.Write(buffer, 0, count);
                                goto Label_0116;
                            }
                        Label_0138:
                            stream.Position = 0;
                            action(stream);
                        }
                    }
                }
            }
        }

        private static void ExtractDirectory(DirectoryInfo targetDir, Stream inputStream, string entryName, DateTime entryDate)
        {
            DirectoryInfo info = new DirectoryInfo(Path.Combine(targetDir.FullName, entryName));
            if (!info.Exists)
            {
                info.Create();
                info.LastWriteTime = entryDate;
                info.Refresh();
            }
        }

        private static void ExtractFile(DirectoryInfo targetDir, Stream inputStream, string entryName, DateTime entryDate, long entrySize)
        {
            FileInfo info = new FileInfo(Path.Combine(targetDir.FullName, entryName));
            if (!info.Directory.Exists)
            {
                info.Directory.Create();
                info.Directory.LastWriteTime = entryDate;
                info.Directory.Refresh();
            }

            using (FileStream stream = new FileStream(info.FullName, FileMode.Create, FileAccess.Write))
            {
                int count = 0x800;
                byte[] buffer = new byte[0x800];
                if (entrySize <= 0L)
                {
                    goto Label_0138;
                }
            Label_0116:
                count = inputStream.Read(buffer, 0, buffer.Length);
                if (count > 0)
                {
                    stream.Write(buffer, 0, count);
                    goto Label_0116;
                }
            Label_0138:
                stream.Close();
            }
            info.LastWriteTime = entryDate;
        }
    }
}
