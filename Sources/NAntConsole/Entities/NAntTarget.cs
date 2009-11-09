using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.Entities
{
    public class NAntTarget
    {
        public NAntTarget(NAntProject project, string name)
        {
            this.project = project;
            this.name = name;
        }

        private readonly NAntProject project;

        public NAntProject Project
        {
            get { return project; }
        }

        private readonly string name;

        public string Name
        {
            get { return name; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private readonly List<string> dependencies = new List<string>();

        public IList<string> Dependencies
        {
            get { return dependencies; }
        }
    }
}
