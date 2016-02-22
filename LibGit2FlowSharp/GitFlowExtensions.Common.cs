using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using LibGit2FlowSharp.Enums;

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

        internal static string GetPrefixByBranch(this Flow gitFlow, FlowBranch branch)
        {
            return gitFlow.Repository.Config.Get<string>($"gitflow.prefix.{Enum.GetName(typeof(FlowBranch), branch)?.ToLower()}")?.Value;
        }
        public static string CurrentBranchLeafName(this Flow gitflow)
        {
            var repo = gitflow.Repository;
            string fullBranchName = repo.Head.Name;
            ConfigurationEntry<string> prefix = null;

            if (gitflow.IsOnFeatureBranch())
            {
                prefix = repo.Config.Get<string>("gitflow.prefix.feature");
            }
            if (gitflow.IsOnReleaseBranch())
            {
                prefix = repo.Config.Get<string>("gitflow.prefix.release");
            }
            if (gitflow.IsOnHotfixBranch())
            {
                prefix = repo.Config.Get<string>("gitflow.prefix.hotfix");
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
