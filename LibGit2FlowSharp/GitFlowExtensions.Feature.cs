
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
            if (!gitFlow.IsOnDevelopBranch())
            {
                var currentBranch = gitFlow.Repository.Checkout(gitFlow.Repository.Branches[gitFlow.GetPrefixByBranch(GitFlowSetting.Develop)]);
                //TODO: Handle non-clean checkout exceptions
                
                // Don't know if this check really is needed
                if (currentBranch?.FriendlyName != gitFlow.GetPrefixByBranch(GitFlowSetting.Develop))
                    return false;
            }

            var newBranch = gitFlow.Repository.CreateBranch($"{gitFlow.GetPrefixByBranch(GitFlowSetting.Feature)}{nameOfFeature}");
            return (gitFlow.Repository.Checkout(newBranch) != null);
        }

        public static bool CompleteFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
        }
    }
}
