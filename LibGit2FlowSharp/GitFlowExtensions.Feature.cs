
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

        public static bool StartNewFeature(this Flow gitFlow, string nameOfFeature, bool fetchRemoteFirst=false)
        {
            return (gitFlow.StartNewBranch(GitFlowSetting.Develop,GitFlowSetting.Feature,nameOfFeature,fetchRemoteFirst) != null);
        }

        public static bool CompleteFeature(this Flow gitFlow, string nameOfFeature)
        {
            throw new NotImplementedException();
        }
    }
}
