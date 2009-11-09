using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CDS.Framework.Tools.NAntConsoleUpdater
{
    public class UpdateArgs
    {
        private readonly Guid productCode;
        private readonly FileInfo updateFile;
        private readonly DirectoryInfo targetDir;
        private readonly Version targetVersion;

        public UpdateArgs(Guid productCode, FileInfo updateFile, DirectoryInfo targetDir, Version targetVersion)
        {
            this.productCode = productCode;
            this.updateFile = updateFile;
            this.targetDir = targetDir;
            this.targetVersion = targetVersion;
        }

        public UpdateArgs(string[] args)
        {
            this.productCode = new Guid(args[0]);
            this.updateFile = new FileInfo(args[1]);
            this.targetDir = new DirectoryInfo(args[2]);
            this.targetVersion = new Version(args[3]);
        }

        public Version TargetVersion
        {
            get { return targetVersion; }
        }

        public DirectoryInfo TargetDir
        {
            get { return targetDir; }
        }

        public FileInfo UpdateFile
        {
            get { return updateFile; }
        }

        public Guid ProductCode
        {
            get { return productCode; }
        }
    }
}
