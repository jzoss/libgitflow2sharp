using LibGit2FlowSharp.Enums;
using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsOnDevelopBranch(this Flow gitFlow)
        {
            return IsOnSpecifiedBranch(gitFlow, GitFlowSetting.Develop);
        }


        //public static bool PullDevelopBranch(this Flow gitFlow)
        //{
        //    PullOptions options = new PullOptions();
        //    options.
        //   gitFlow.Repository.Network.Pull()
        //}


    }
}
