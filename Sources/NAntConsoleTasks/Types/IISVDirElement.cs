using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types
{
    public class IISVDirElement : Element
    {
        private string vdirName;
        [TaskAttribute("name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string VDirName
        {
            get { return vdirName; }
            set { vdirName = value; }
        }

        private bool createApp = false;
        [TaskAttribute("createApp")]
        public bool CreateApp
        {
            get { return createApp; }
            set { createApp = value; }
        }

        private readonly ArrayList vdirProperties = new ArrayList();
        [BuildElementArray("property", ElementType = typeof(IISTypedPropertyElement))]
        public ArrayList VDirProperties
        {
            get
            {
                return vdirProperties;
            }
        }

        private readonly ArrayList vdirs = new ArrayList();
        [BuildElementArray("vdir", ElementType = typeof(IISVDirElement))]
        public ArrayList VDirs
        {
            get
            {
                return vdirs;
            }
        }
    }
}
