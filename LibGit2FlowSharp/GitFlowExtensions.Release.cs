using LibGit2FlowSharp.Enums;
using System;
using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsOnReleaseBranch(this Flow gitFlow)
        {
            return IsOnSpecifiedBranch(gitFlow, GitFlowSetting.Release);
        }

        public static bool StartNewRelease(this Flow gitFlow, string nameOfRelease)
        {
            if (!gitFlow.IsOnDevelopBranch())
                gitFlow.Repository.Checkout(gitFlow.Repository.Branches[gitFlow.GetPrefixByBranch(FlowBranch.Develop)]);
        
            var newBranch = gitFlow.Repository.CreateBranch($"{gitFlow.GetPrefixByBranch(FlowBranch.Feature)}/{nameOfRelease}");
            return true;
        }

        public static bool CompleteRelease(this Flow gitFlow, string nameOfRelease)
        {
            throw new NotImplementedException();
        }
    }
}
