
using LibGit2FlowSharp.Enums;

using System;
using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsOnFeatureBranch(this Flow gitFlow)
        {
            return IsOnSpecifiedBranch(gitFlow, GitFlowSetting.Feature);
        }

        public static bool StartNewFeature(this Flow gitFlow, string nameOfFeature)
        {
            var newBranch = gitFlow.Repository.CreateBranch($"{gitFlow.GetPrefixByBranch(FlowBranch.Feature)}/{nameOfFeature}");           
            return true;
        }

        public static bool CompleteFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
        }
    }
}
