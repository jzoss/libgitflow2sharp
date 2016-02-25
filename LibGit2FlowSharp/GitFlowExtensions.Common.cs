using LibGit2FlowSharp.Attributes;
using LibGit2Sharp;
using LibGit2FlowSharp.Enums;
using LibGit2FlowSharp.Extensions;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsOnMasterBranch(this Flow gitflow)
        {
           return IsOnSpecifiedBranch(gitflow, GitFlowSetting.Master);
        }

        internal static bool IsOnSpecifiedBranch(this Flow gitFlow, GitFlowSetting setting)
        {
            var repo = gitFlow.Repository;
            if (!gitFlow.IsInitialized())
                return false;
            var featurePrefix = repo.Config.Get<string>(GetConfigKey(setting));
            return featurePrefix != null && repo.Head.FriendlyName.StartsWith(featurePrefix.Value);
        }

        internal static string GetConfigKey(GitFlowSetting setting)
        {
            return setting.GetAttribute<GitFlowConfigAttribute>().ConfigName;
        }

        internal static string GetPrefixByBranch(this Flow gitFlow, GitFlowSetting branch)
        {
            return gitFlow.Repository.Config.Get<string>(branch.GetAttribute<GitFlowConfigAttribute>().ConfigName)?.Value;
        }
        public static string CurrentBranchLeafName(this Flow gitflow)
        {
            var repo = gitflow.Repository;
            var fullBranchName = repo.Head.CanonicalName;
            ConfigurationEntry<string> prefix = null;

            if (gitflow.IsOnFeatureBranch())
            {
                prefix = repo.Config.Get<string>(GitFlowSetting.Feature.GetAttribute<GitFlowConfigAttribute>().ConfigName);
            }
            if (gitflow.IsOnReleaseBranch())
            {
                prefix = repo.Config.Get<string>(GitFlowSetting.Release.GetAttribute<GitFlowConfigAttribute>().ConfigName);
            }
            if (gitflow.IsOnHotfixBranch())
            {
                prefix = repo.Config.Get<string>(GitFlowSetting.HotFix.GetAttribute<GitFlowConfigAttribute>().ConfigName);
            }
            return prefix != null ? fullBranchName.Replace(prefix.Value, "") : fullBranchName;
        }

        public static string CurrentStatus(this Flow gitflow)
        {
            var leafName = gitflow.CurrentBranchLeafName();
            string status = "";
            if (gitflow.IsOnDevelopBranch())
                status = "Develop: " + leafName;
            else if (gitflow.IsOnFeatureBranch())
                status = "Feature: " + leafName;
            else if (gitflow.IsOnHotfixBranch())
                status = "Hotfix: " + leafName;
            else if (gitflow.IsOnReleaseBranch())
                status = "Release: " + leafName;

            return status;
        }
    }
}
