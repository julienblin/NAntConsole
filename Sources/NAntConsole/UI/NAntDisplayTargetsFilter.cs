using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CDS.Framework.Tools.NAntConsole.Entities;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Composite;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    internal class NAntDisplayTargetsFilter
    {
        private readonly Regex rePreBuildFilter;
        private readonly Regex rePostBuildFilter;
        private readonly Regex reDiscardFilter;

        public NAntDisplayTargetsFilter()
        {
            rePreBuildFilter = new Regex(string.Concat("^", CompositeConstants.PRE_BUILD_TARGET_PREFIX, ".+$"), RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);
            rePostBuildFilter = new Regex(string.Concat("^", CompositeConstants.POST_BUILD_TARGET_PREFIX, ".+$"), RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);
            reDiscardFilter = new Regex(string.Concat("^", CompositeConstants.DISCARD_TARGET_PREFIX, ".+$"), RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);
        }

        public bool Display(NAntTarget target)
        {
            if (rePreBuildFilter.Match(target.Name).Success)
                return false;
             
            if (rePostBuildFilter.Match(target.Name).Success)
                return false;

            if (reDiscardFilter.Match(target.Name).Success)
                return false;

            return true;
        }
    }
}
