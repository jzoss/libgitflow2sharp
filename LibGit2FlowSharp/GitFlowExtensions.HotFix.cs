using LibGit2FlowSharp.Enums;
using System;
using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions 
    {
        public static bool IsOnHotfixBranch(this Flow gitFlow)
        {
            return IsOnSpecifiedBranch(gitFlow, GitFlowSetting.HotFix);
        }

        public static bool StartNewHotfix(this Flow gitFlow, string nameOfHotfix )
        {
            var newBranch = gitFlow.Repository.CreateBranch($"{gitFlow.GetPrefixByBranch(FlowBranch.Feature)}/{nameOfHotfix}");
            return true;            
        }

        public static bool CompleteHotfix(this Flow gitFlow, string nameOfHotfix)
        {
            throw new NotImplementedException();
        }

    }
}
