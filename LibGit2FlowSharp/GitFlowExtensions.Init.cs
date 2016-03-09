using System;
using System.Linq;
using LibGit2FlowSharp.Attributes;
using LibGit2FlowSharp.Enums;
using LibGit2FlowSharp.Extensions;
using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static void Init(this Flow gitFlow, GitFlowRepoSettings settings, Signature author)
        {
            var repo = gitFlow.Repository;
            //Only init if it is not initialized 
            if (gitFlow.IsInitialized())
                //TODO: Does Init do anything if already initialized? Is it sufficient to just check the ConfigValues? Should it check branch existance as well?
                return;

            if (gitFlow.IsEmptyRepository())
            {
                //TODO: Decide if Init should create necesarry branches too?
                //Create Master branch 
                Signature committer = author;
                CommitOptions opts = new CommitOptions();
                opts.AllowEmptyCommit = true;
                repo.Commit("Initial commit", author, committer, opts);
                //Now make Dev
            }

            SetupMasterBranch(repo, settings.Settings[GitFlowSetting.Master], author);

            //TODO.. Repo has branches 

            settings.Settings
                    .ToList()
                    .ForEach(item =>
                        repo.Config.Set(GetConfigKey(item.Key), item.Value, settings.ConfigurationLocation)
                    );
        }

        public static void UnInit(this Flow gitFlow)
        {
            //Todo: Decide if there should be a Cleanup function to remove the config settings 
        }

        //private void SetConfigValue(Repository repository, ) 
        /// <summary>
        /// Function to check if the bare minimum of config values are present
        /// </summary>
        /// <param name="gitFlow"></param>
        /// <returns>bool - True if OK</returns>
        public static bool IsInitialized(this Flow gitFlow)
        {
            var repo = gitFlow.Repository;
            var requiredBranches = new[] { GitFlowSetting.Develop, GitFlowSetting.Master, GitFlowSetting.Release };

            return
                requiredBranches.Select(branch => branch.GetAttribute<GitFlowConfigAttribute>().ConfigName)
                    .All(configKey => repo.Config.Any(configEntry => configEntry.Key == configKey));
        }

        /// <summary>
        /// Function to see if the Repository is empty or has commit
        /// </summary>
        /// <param name="gitFlow"></param>
        /// <returns>bool - true if no branches have been generated.</returns>
        public static bool IsEmptyRepository(this Flow gitFlow)
        {
            return !gitFlow.Repository.Branches.Any();
        }

        #region private init methods

        private static void SetupMasterBranch(Repository repository, string masterName, Signature author)
        {
            //We know we have a master branch , so check to see if the gitflow master is different and if it is 
            if (masterName != "master" && repository.Branches[masterName] != null)
            {
                repository.CreateBranch(masterName);
            }
        }

        private static void CreateDev(Repository repository, string branchName, Signature author)
        {
            Signature committer = author;
            CommitOptions opts = new CommitOptions();
            opts.AllowEmptyCommit = true;

            repository.Commit("Initial commit", author, committer, opts);
        }

        private static void GetRemoteBranch()
        {
            
        }

        #endregion
    }

}
