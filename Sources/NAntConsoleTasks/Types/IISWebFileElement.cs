using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using System.Collections;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class IISWebFileElement : Element
    {
        private string path;
        [TaskAttribute("path", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private string fileName;
        [TaskAttribute("fileName", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private readonly ArrayList webFileProperties = new ArrayList();
        [BuildElementArray("property", ElementType = typeof(IISTypedPropertyElement))]
        public ArrayList WebFileProperties
        {
            get
            {
                return webFileProperties;
            }
        }
    }
}
