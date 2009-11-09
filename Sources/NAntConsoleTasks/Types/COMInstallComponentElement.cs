using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;
using NAnt.Core.Types;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class COMInstallComponentElement : Element
    {
        private FileInfo file;
        [TaskAttribute("file")]
        public FileInfo File
        {
            get { return file; }
            set { file = value; }
        }

        private bool net;
        [TaskAttribute("net")]
        public bool Net
        {
            get { return net; }
            set { net = value; }
        }

        private FileSet fileSet;
        [BuildElement("fileset")]
        public FileSet FileSet
        {
            get { return fileSet; }
            set { fileSet = value; }
        }
    }
}
