using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

        internal static Branch GetBranch(this Flow gitFlow, GitFlowSetting branch, string leafname="")
        {
            return gitFlow.Repository.Branches[gitFlow.GetPrefixByBranch(branch) + leafname];
        }

        internal static Branch StartNewBranch(this Flow gitFlow, GitFlowSetting originationBranch,GitFlowSetting branchBase, string leafname, bool shouldFetchRemote=false, bool trackRemote = false)
        {
            //TODO: Handle fetching from remote 
            if (!IsOnSpecifiedBranch(gitFlow, originationBranch))
            {
                var currentBranch = gitFlow.Repository.Checkout(gitFlow.GetBranch(originationBranch));
                //TODO: Handle non-clean checkout exceptions

                // Don't know if this check really is needed
                if (currentBranch?.FriendlyName != gitFlow.GetPrefixByBranch(originationBranch))
                    return null;
            }
            var newBranchName = $"{gitFlow.GetPrefixByBranch(branchBase)}{leafname}";
            var newBranch = gitFlow.Repository.AddGetBranch(newBranchName,track:trackRemote);
            //var newBranch = gitFlow.Repository.CreateBranch($"{gitFlow.GetPrefixByBranch(branchBase)}{leafname}");
            return gitFlow.Repository.Checkout(newBranch);
        }

        #region Name Helper Functions

        internal static string GetFullBranchName(this Flow gitFlow,GitFlowSetting branch, string leafname)
        {
            return GetFullBranchName(gitFlow.GetPrefixByBranch(branch), leafname);
        }

        internal static string GetFullBranchName(string prefix, string leafname)
        {
            return ($"{prefix}{leafname}");
        }

        #endregion

        #region Common Flow Functions

        internal static bool Publish(this Flow gitFlow, string branchName, string remoteName = "origin")
        {
            var repo = gitFlow.Repository;
            Remote remote = repo.Network.Remotes[remoteName];

            try
            {
                gitFlow.Repository.Network.Fetch(remote);
                Branch branch;
                if (repo.TryGetRemoteBranch(branchName, out branch))
                {
                    GitFlowExtensions.Log($"Branch {branchName} already published");
                    return false;
                }
                else if (repo.TryGetLocalBranch(branchName, out branch))
                {
                    if (!branch.IsTracking)
                    {
                        repo.SetRemoteBranch(branch, remoteName);
                    }
                    repo.Network.Push(branch);
                    GitFlowExtensions.Log($"Branch {branchName} Published");
                    repo.TryCheckout(branch);
                    return true;
                }
                else
                {
                    GitFlowExtensions.LogError("Publish Error, Branch Not Found");
                }
                return false;
            }
            catch (Exception ex)
            {

                GitFlowExtensions.LogError("Publish Error ", ex.Message);
                return false;
            }
        }

        internal static bool Track(this Flow gitFlow, string branchName)
        {
            var repo = gitFlow.Repository;
            Branch branch;
            if (repo.TryGetLocalBranch(branchName, out branch))
            {
                GitFlowExtensions.Log($"Branch {branchName} already tracked");
            }
            else if (repo.TryGetRemoteBranch(branchName, out branch))
            {
                repo.TryCheckout(branch);
                GitFlowExtensions.Log($"Now Tracking {branchName}");
                return true;
            }
            else
            {
                GitFlowExtensions.LogError("Track Error, Branch Not Found");
            }
            return false;
        }


        internal static IEnumerable<BranchInfo> GetAllBranchesByPrefix(this Flow gitFlow, GitFlowSetting setting)
        {
            var prefix = gitFlow.GetPrefixByBranch(setting);
            return gitFlow.GetAllBranchesByPrefix(prefix);
        }

        internal static IEnumerable<BranchInfo> GetAllBranchesByPrefix(this Flow gitFlow, string prefix)
        {
            var repo = gitFlow.Repository;

            if (gitFlow.IsInitialized())
            {
                
                var featureBranches =
                       repo.Branches.Where(b => !b.IsRemote && b.FriendlyName.StartsWith(prefix))
                           .Select(c => new BranchInfo(c, prefix)).ToList();

                var remoteFeatureBranches =
                    repo.Branches.Where(b => b.IsRemote && b.FriendlyName.Contains(prefix)
                    && !repo.Branches.Any(br => !br.IsRemote && br.IsTracking && br.TrackedBranch.CanonicalName == b.CanonicalName))
                      .Select(c => new BranchInfo(c, prefix)).ToList();

                featureBranches.AddRange(remoteFeatureBranches);
                return featureBranches;
            }
            return new List<BranchInfo>();
        }

        #endregion


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
