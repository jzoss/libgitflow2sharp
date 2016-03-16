
using LibGit2FlowSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsOnFeatureBranch(this Flow gitFlow)
        {
            return IsOnSpecifiedBranch(gitFlow, GitFlowSetting.Feature);
        }

        public static BranchInfo StartNewFeature(this Flow gitFlow, string nameOfFeature, bool fetchRemoteFirst=false)
        {
            var branch = gitFlow.StartNewBranch(GitFlowSetting.Develop, GitFlowSetting.Feature, nameOfFeature,
                fetchRemoteFirst);

            return new BranchInfo(branch, gitFlow.Prefix.Feature);
        }


        public static bool FinishFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
        }

        public static bool PublishFeature(this Flow gitFlow, string nameOfFeature)
        {
            var fullName = GetFullBranchName(gitFlow.Prefix.Feature, nameOfFeature);
            return gitFlow.Publish(fullName);
        }

        public static bool TrackFeature(this Flow gitFlow, string nameOfFeature)
        {
            var fullName = GetFullBranchName(gitFlow.Prefix.Feature, nameOfFeature);
            return gitFlow.Track(fullName);
        }

        public static bool DiffFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
        }

        public static bool RebaseFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
        }

        public static bool CheckoutFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
        }

        public static bool PullFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
        }


        public static IEnumerable<BranchInfo> GetAllFeatureBranches(this Flow gitFlow)
        {
            return gitFlow.GetAllBranchesByPrefix(gitFlow.Prefix.Feature);
        }
    }
}
