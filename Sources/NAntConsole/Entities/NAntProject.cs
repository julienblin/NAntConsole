using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.Entities
{
    public class NAntProject
    {
        public NAntProject(FileInfo buildFile)
        {
            this.buildFile = buildFile;
        }

        readonly FileInfo buildFile;

        public FileInfo BuildFile
        {
            get { return buildFile; }
        }

        private string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        private string defaultTargetName;

        public string DefaultTargetName
        {
            get { return defaultTargetName; }
            set { defaultTargetName = value; }
        }

        private readonly List<NAntTarget> targets = new List<NAntTarget>();

        public IList<NAntTarget> Targets
        {
            get { return targets; }
        }

        public NAntTarget FindTargetByName(string name)
        {
            foreach (NAntTarget target in targets)
            {
                if (target.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return target;
                }
            }
            return null;
        }
    }
}
