using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2FlowSharp.Enums;

namespace LibGit2FlowSharp
{
    internal class Prefix
    {
        private Flow _flow;

        public Prefix(Flow flow)
        {
            _flow = flow;
        }

        internal string Develop => _flow.GetPrefixByBranch(GitFlowSetting.Develop);
        
        internal string BugFix => _flow.GetPrefixByBranch(GitFlowSetting.BugFix);
        internal string Feature => _flow.GetPrefixByBranch(GitFlowSetting.Feature);
        internal string HotFix => _flow.GetPrefixByBranch(GitFlowSetting.HotFix);
        internal string Master => _flow.GetPrefixByBranch(GitFlowSetting.Master);
        internal string Release => _flow.GetPrefixByBranch(GitFlowSetting.Release);
        internal string Support => _flow.GetPrefixByBranch(GitFlowSetting.Support);
        internal string VersionTag => _flow.GetPrefixByBranch(GitFlowSetting.VersionTag);
    }
}
