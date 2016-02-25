using LibGit2FlowSharp.Enums;
using System;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsOnReleaseBranch(this Flow gitFlow)
        {
            return IsOnSpecifiedBranch(gitFlow, GitFlowSetting.Release);
        }

        public static bool StartNewRelease(this Flow gitFlow, string nameOfRelease, bool fetchRemoteFirst=false)
        {
            return (gitFlow.StartNewBranch(GitFlowSetting.Develop, GitFlowSetting.Release, nameOfRelease,fetchRemoteFirst) != null);
        }

        public static bool CompleteRelease(this Flow gitFlow, string nameOfRelease)
        {
            throw new NotImplementedException();
        }
    }
}
