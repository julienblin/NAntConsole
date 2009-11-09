using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite
{
    public static class CompositeConstants
    {
        public const string ENV_DIRECTORY_NAME = @"Environment";
        public const string NANTCONSOLE_VERSION_FILE_NAME = @"NAntConsole.version";
        public const string DEPLOY_FILE_NAME = @"deploy.nant";

        public const string PRE_BUILD_TARGET_PREFIX = @"pre-build-";
        public const string POST_BUILD_TARGET_PREFIX = @"post-build-";
        public const string DISCARD_TARGET_PREFIX = @"\*\*-";
    }
}
