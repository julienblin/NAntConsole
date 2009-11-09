using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NAnt.Core;
using NAnt.Core.Attributes;
using SharpSvn;
using System.Diagnostics;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.SVN
{
    [TaskName("svn-delete")]
    public class SVNDeleteTask : BaseSVNMessageTask
    {
        private Uri svnUri;
        [TaskAttribute("uri", Required = true)]
        public Uri SvnUri
        {
            get { return svnUri; }
            set { svnUri = value; }
        }


        protected override void ExecuteSVNTask(SvnClient client)
        {
            SvnCommitResult result;
            SvnDeleteArgs args = new SvnDeleteArgs();

            args.LogMessage = Message;
            args.ThrowOnError = true;
            
            Log(Level.Info, Resources.SVNDelete, SvnUri);

            client.RemoteDelete(svnUri, args, out result);
        }
    }
}
