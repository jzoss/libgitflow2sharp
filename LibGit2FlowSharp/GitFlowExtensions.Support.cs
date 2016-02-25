using LibGit2FlowSharp.Enums;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsOnSupportBranch(this Flow gitFlow)
        {
            return IsOnSpecifiedBranch(gitFlow, GitFlowSetting.Develop);
        }
    }
}
