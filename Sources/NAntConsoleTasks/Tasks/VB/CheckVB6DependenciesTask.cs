using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.VB
{
    [TaskName("check-vb6-dependencies")]
    public class CheckVB6DependenciesTask : Task
    {
        static Regex reReference = new Regex("Reference=\\*\\\\G\\{(?<guid>[0-9A-Za-z\\-]+)\\}#(?<version>[0-9\\.]+)#[0-9]#(?<path>[^#]+)#(?<description>.+)", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);

        readonly List<string> pathExceptions = new List<string>();

        static class NativeMethods
        {
            [DllImport("oleaut32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern int LoadTypeLib(string filename, ref IntPtr pTypeLib);

            [DllImport("Kernel32.dll")]
            public static extern int FormatMessage(int flags, IntPtr source, int messageId, int languageId, StringBuilder buffer, int size, IntPtr arguments);
        }

        public CheckVB6DependenciesTask()
        {
            pathExceptions.Add(Environment.SystemDirectory);
            pathExceptions.Add(Environment.GetEnvironmentVariable("ProgramFiles"));
        }

        private FileInfo vbProject;
        [TaskAttribute("project", Required = true)]
        public FileInfo VbProject
        {
            get { return vbProject; }
            set { vbProject = value; }
        }

        protected override void ExecuteTask()
        {
            if (!VbProject.Exists)
            {
                throw new BuildException(string.Format(Resources.MissingFile, VbProject.FullName));
            }

            Log(Level.Info, string.Format(Resources.CheckVB6DependenciesCheck, VbProject.Name));
            string vbProjContent = File.ReadAllText(VbProject.FullName, Encoding.GetEncoding(@"ISO-8859-1"));

            IList<Reference> references = FindReferences(vbProjContent);
            foreach (Reference reference in references)
            {
                bool skipped = false;
                foreach (string pathException in pathExceptions)
                {
                    if (reference.Path.StartsWith(pathException, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Log(Level.Verbose, Resources.CheckVB6DependenciesSkippingDirectory, reference.Path);
                        skipped = true;
                    }
                }

                if (!skipped)
                {
                    CheckPathRooted(reference);
                    CheckPathReference(reference);
                    CheckReferencInProjectFolder(reference);
                    CheckClassId(reference);
                }
            }
        }

        private void CheckPathRooted(Reference reference)
        {
            if (Path.IsPathRooted(reference.Path))
            {
                throw new BuildException(string.Format(Resources.CheckVB6DependenciesPathRooted, reference.Description, VbProject.Name, reference.Path));
            }
        }

        private void CheckPathReference(Reference reference)
        {
            string referencePath = Path.Combine(VbProject.Directory.FullName, reference.Path);
            reference.File = new FileInfo(referencePath);

            if (!reference.File.Exists)
            {
                throw new BuildException(string.Format(Resources.CheckVB6DependenciesReferenceMissing, reference.Description, VbProject.Name, reference.Path));
            }
        }

        private void CheckReferencInProjectFolder(Reference reference)
        {
            string referenceFullPath = Path.GetFullPath(Path.Combine(VbProject.Directory.FullName, reference.Path));

            if(!referenceFullPath.StartsWith(Project.BaseDirectory))
            {
                throw new BuildException(string.Format(Resources.CheckVB6DependenciesReferenceNotInProjectFolder, reference.Description, vbProject.Name, reference.Path));
            }
        }

        private void CheckClassId(Reference reference)
        {
            System.Runtime.InteropServices.ComTypes.TYPELIBATTR typeLibAttr = GetTypeLibAttr(reference);
            if (!typeLibAttr.guid.Equals(reference.Guid))
            {
                throw new BuildException(string.Format(Resources.CheckVB6DependenciesGuidNotMatch, reference.Description, VbProject.Name, typeLibAttr.guid, reference.Guid));
            }
            string typeLibVersion = string.Concat(typeLibAttr.wMajorVerNum.ToString(), ".", typeLibAttr.wMinorVerNum.ToString());
            if (!typeLibVersion.Equals(reference.Version, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new BuildException(string.Format(Resources.CheckVB6DependenciesWrongVersion, reference.Description, VbProject.Name, typeLibVersion, reference.Version));
            }
        }

        private static IList<Reference> FindReferences(string content)
        {
            List<Reference> result = new List<Reference>();
            using (StringReader reader = new StringReader(content))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    Match mReference = reReference.Match(line);
                    if (mReference.Success)
                    {
                        Reference reference = new Reference();
                        reference.Guid = new Guid(mReference.Groups["guid"].Value);
                        reference.Version = mReference.Groups["version"].Value;
                        reference.Path = mReference.Groups["path"].Value;
                        reference.Description = mReference.Groups["description"].Value;
                        result.Add(reference);
                    }
                    line = reader.ReadLine();
                }
            }

            return result;
        }

        private System.Runtime.InteropServices.ComTypes.TYPELIBATTR GetTypeLibAttr(Reference reference)
        {
            IntPtr pTypeLib = new IntPtr(0);
            int error = 0;
            int num2 = NativeMethods.LoadTypeLib(reference.File.FullName, ref pTypeLib);
            error = Marshal.GetLastWin32Error();
            if ((error != 0) || (num2 != 0))
            {
                int num3 = (error != 0) ? error : num2;
                throw new BuildException(string.Format("Error loading typelib '{0}' ({1}: {2}).", reference.File.FullName, num3, GetWin32ErrorMessage(num3)), this.Location);
            }

            ITypeLib typeLib = null;
            try
            {
                typeLib = (ITypeLib)Marshal.GetTypedObjectForIUnknown(pTypeLib, typeof(ITypeLib));
                error = Marshal.GetLastWin32Error();
                if (error != 0)
                {
                    throw new BuildException(string.Format("Error retrieving information from typelib '{0}' ({1}: {2}).", reference.File.FullName, error, GetWin32ErrorMessage(error)), this.Location);
                }

                IntPtr pLibAttr = new IntPtr(0);
                typeLib.GetLibAttr(out pLibAttr);
                return (System.Runtime.InteropServices.ComTypes.TYPELIBATTR)Marshal.PtrToStructure(pLibAttr, typeof(System.Runtime.InteropServices.ComTypes.TYPELIBATTR));
            }
            finally
            {
                if (typeLib != null)
                {
                    Marshal.ReleaseComObject(typeLib);
                }
            }
        }

        private static string GetWin32ErrorMessage(int error)
        {
            StringBuilder buffer = new StringBuilder(0x400);
            if (NativeMethods.FormatMessage(0x1000, IntPtr.Zero, error, 0, buffer, buffer.Capacity, IntPtr.Zero) != 0)
            {
                char[] trimChars = new char[3];
                trimChars[1] = '\n';
                trimChars[2] = '\r';
                return buffer.ToString().TrimEnd(trimChars);
            }
            return string.Empty;
        }


        class Reference
        {
            public Guid Guid;
            public string Version;
            public string Path;
            public FileInfo File;
            public string Description;
        }
    }
}
