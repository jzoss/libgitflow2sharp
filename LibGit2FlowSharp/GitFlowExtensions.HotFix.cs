using LibGit2FlowSharp.Enums;
using System;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions 
    {
        public static bool IsOnHotfixBranch(this Flow gitFlow)
        {
            return IsOnSpecifiedBranch(gitFlow, GitFlowSetting.HotFix);
        }

        public static bool StartNewHotfix(this Flow gitFlow, string nameOfHotfix, bool fetchRemoteFirst=false)
        {
            return (gitFlow.StartNewBranch(GitFlowSetting.Master, GitFlowSetting.HotFix, nameOfHotfix, fetchRemoteFirst)!=null);
        }

        public static bool FinishHotfix(this Flow gitFlow, string nameOfHotfix)
        {
            throw new NotImplementedException();
        }

        public static bool PublishHotfix(this Flow gitFlow, string nameOfHotfix)
        {
            throw new NotImplementedException();
        }

        public static bool TrackHotfix(this Flow gitFlow, string nameOfHotfix)
        {
            throw new NotImplementedException();
        }

    }
}
