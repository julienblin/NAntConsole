using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows
{
    public abstract class BaseAccessRuleTask : Task
    {
        private DirectoryInfo dir;
        [TaskAttribute("dir")]
        public DirectoryInfo Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        private FileInfo targetFile;
        [TaskAttribute("file")]
        public FileInfo TargetFile
        {
            get { return targetFile; }
            set { targetFile = value; }
        }

        private string share;
        [TaskAttribute("share")]
        public string Share
        {
            get { return share; }
            set { share = value; }
        }

        private FileSet targetFiles;
        [BuildElement("fileset")]
        public FileSet TargetFiles
        {
            get { return targetFiles; }
            set { targetFiles = value; }
        }

        private string ntaccount;
        [TaskAttribute("ntaccount", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string NTAccount
        {
            get { return ntaccount; }
            set { ntaccount = value; }
        }

        protected override void ExecuteTask()
        {
            if (Dir != null)
            {
                if (Dir.Exists)
                {
                    ExecuteOnDir(Dir);
                }
                else
                {
                    throw new BuildException(string.Format(Resources.BaseAccessRuleDirNotFound, Dir.FullName));
                }
            }

            if (TargetFile != null)
            {
                if (TargetFile.Exists)
                {
                    ExecuteOnFile(TargetFile);
                }
                else
                {
                    throw new BuildException(string.Format(Resources.BaseAccessRuleFileNotFound, TargetFile.FullName));
                }
            }

            if (TargetFiles != null)
            {
                foreach (string fileName in TargetFiles.FileNames)
                {
                    FileInfo fi = new FileInfo(fileName);
                    if (fi.Exists)
                    {
                        ExecuteOnFile(fi);
                    }
                    else
                    {
                        throw new BuildException(string.Format(Resources.BaseAccessRuleFileNotFound, fileName));
                    }
                }
            }

            if (!string.IsNullOrEmpty(Share))
            {
                ExecuteOnShare(Share);
            }
        }

        protected abstract void ExecuteOnDir(DirectoryInfo dir);

        protected abstract void ExecuteOnFile(FileInfo file);

        protected abstract void ExecuteOnShare(string share);

        protected IList<FileSystemAccessRule> FindAccessRules(FileSystemSecurity security)
        {
            System.Security.Principal.NTAccount typedAccount = GetNTAccount();
            List<FileSystemAccessRule> result = new List<FileSystemAccessRule>();
            foreach (FileSystemAccessRule fileSystemAccessRule in security.GetAccessRules(true, false, typeof(NTAccount)))
            {
                if (fileSystemAccessRule.IdentityReference.Value.Equals(typedAccount.Value,
                                                                        StringComparison.InvariantCultureIgnoreCase))
                {
                    result.Add(fileSystemAccessRule);
                }
            }
            return result;
        }

        private NTAccount GetNTAccount()
        {
            string realAccountNameWithDomain = NTAccount.Contains(@"\")
                                                   ? NTAccount
                                                   : string.Concat(Environment.MachineName, @"\", NTAccount);
            return new NTAccount(realAccountNameWithDomain);
        }
    }
}
