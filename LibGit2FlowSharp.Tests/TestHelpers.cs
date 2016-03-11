using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibGit2FlowSharp.Enums;
using LibGit2Sharp;

namespace LibGit2FlowSharp.Tests
{
    internal static class TestHelpers
    {
        internal static void DeleteBranch(Repository repo, Branch branch)
        {
            if (branch == null)
                return;
            if (branch.IsCurrentRepositoryHead)
                repo.Checkout(repo.Branches[repo.Flow().GetPrefixByBranch(GitFlowSetting.Master)]);
            repo.Branches.Remove(branch);
        }

        internal static string CreateEmptyRepo(string basepath,string dirName = "emptyRepo")
        {
            var path = Path.Combine(basepath, dirName);
            Directory.CreateDirectory(path);
            Thread.Sleep(1000);
            Repository.Init(path);
            return path;
        }

        internal static void CreateLocalTestBranch(Repository repo, string branchName, GitFlowSetting setting, GitFlowRepoSettings repoConfig)
        {
            if (!string.IsNullOrWhiteSpace(branchName))
            {
                repoConfig.SetSetting(setting, branchName);
                repo.CreateBranch(branchName);
            }

        }

        internal static void CleanRepoDir(string basepath,string dirName = "emptyRepo")
        {
            var path = Path.Combine(basepath, dirName);
            DeleteReadOnlyDirectory(path);

        }

        public static void DeleteReadOnlyDirectory(string directory)
        {
            foreach (var subdirectory in Directory.EnumerateDirectories(directory))
            {
                DeleteReadOnlyDirectory(subdirectory);
            }
            foreach (var fileName in Directory.EnumerateFiles(directory))
            {
                var fileInfo = new FileInfo(fileName);
                fileInfo.Attributes = FileAttributes.Normal;
                fileInfo.Delete();
            }
            Directory.Delete(directory);
        }

    }
}
