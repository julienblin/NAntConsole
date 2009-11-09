using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    public class SvnExplorerSelection
    {
        private readonly string repositoryUri;
        private readonly string svnUri;

        public SvnExplorerSelection(string repositoryUri, string svnUri)
        {
            this.repositoryUri = repositoryUri;
            this.svnUri = svnUri;
        }

        public string SvnUri
        {
            get { return svnUri; }
        }

        public string RepositoryUri
        {
            get { return repositoryUri; }
        }

        public string SvnUriWithoutRepository
        {
            get { return SvnUri.Replace(RepositoryUri, string.Empty); }
        }
    }
}
