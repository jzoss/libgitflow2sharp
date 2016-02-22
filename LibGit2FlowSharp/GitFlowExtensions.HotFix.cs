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
            // TODO: See if these similar methods should use a base metod to reduce code duplication    
            throw new NotImplementedException();
        }

        public static bool CompleteHotfix(this Flow gitFlow, string nameOfHotfix)
        {
            throw new NotImplementedException();
        }

    }
}
