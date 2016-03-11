
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

        public static bool StartNewFeature(this Flow gitFlow, string nameOfFeature, bool fetchRemoteFirst=false)
        {
            return (gitFlow.StartNewBranch(GitFlowSetting.Develop,GitFlowSetting.Feature,nameOfFeature,fetchRemoteFirst) != null);
        }


        public static bool FinishFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
        }

        public static bool PublishFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
        }

        public static bool TrackFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
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
            var repo = gitFlow.Repository;
           
            if (gitFlow.IsInitialized())
            {
                var featurePrefix = gitFlow.GetPrefixByBranch(GitFlowSetting.Feature);
                var featureBranches =
                       repo.Branches.Where(b => !b.IsRemote && b.Name.StartsWith(featurePrefix))
                           .Select(c => new BranchInfo(c,featurePrefix)).ToList();

                var remoteFeatureBranches =
                    repo.Branches.Where(b => b.IsRemote && b.Name.Contains(featurePrefix)
                    && !repo.Branches.Any(br => !br.IsRemote && br.IsTracking && br.TrackedBranch.CanonicalName == b.CanonicalName))
                      .Select(c => new BranchInfo(c, featurePrefix)).ToList();

                featureBranches.AddRange(remoteFeatureBranches);
                return featureBranches;
            }
            return new List<BranchInfo>();
        }
    }
}
