using System;
using System.IO;
using System.Linq;
using System.Threading;
using LibGit2FlowSharp.Attributes;
using LibGit2FlowSharp.Enums;
using LibGit2FlowSharp.Extensions;
using LibGit2Sharp;
using Xunit;

namespace LibGit2FlowSharp.Tests
{
    public class InitTests : IDisposable
    {
        private Repository _testRepository;
        private const string basepath = @"C:\test\";
        private string _testPath;
        Signature author = new Signature("Test", "UnitTest@xunit.com", DateTime.Now);

        public InitTests()
        {
            _testPath = CreateEmptyRepo();
        }

        [Fact]
        public void TestFolderIsAGitRepository() 
        {
            using (_testRepository = new Repository(basepath))
            {
            }
        }

        [Fact]
        public void InitFlowSetsConfigToDefaultValues()
        {
            
            using (_testRepository = new Repository(_testPath))
            {
                _testRepository.Flow().Init(new GitFlowRepoSettings(), author);
                
                //TODO Finish 
                Assert.Equal(
                    _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Master),
                    GitFlowSetting.Master.GetAttribute<GitFlowConfigAttribute>().DefaultValue
                    );
                Assert.Equal(
                    _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Develop),
                    GitFlowSetting.Develop.GetAttribute<GitFlowConfigAttribute>().DefaultValue
                    );
            }
        }

        public void InitFlowCreatesBranches()
        {

            using (_testRepository = new Repository(_testPath))
            {
                _testRepository.Flow().Init(new GitFlowRepoSettings(), author);
                Assert.Equal(_testRepository.Branches.Count(), 2);
                Assert.NotNull(_testRepository.Branches.FirstOrDefault(
                        x => string.Equals(x.FriendlyName, GitFlowSetting.Master.GetAttribute<GitFlowConfigAttribute>().DefaultValue, StringComparison.OrdinalIgnoreCase)));
                Assert.NotNull(_testRepository.Branches.FirstOrDefault(
                        x => string.Equals(x.FriendlyName, GitFlowSetting.Develop.GetAttribute<GitFlowConfigAttribute>().DefaultValue, StringComparison.OrdinalIgnoreCase)));
            }
        }

        [Fact]
        public void FlowIsInitialized()
        {
            using (_testRepository = new Repository(_testPath))
            {
                Assert.True(_testRepository.Flow().IsInitialized());
            }
        }

        [Fact]
        public void TestPrefixIsSet()
        {
            using (_testRepository = new Repository(_testPath))
            {
                var prefix = _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Develop);
                Assert.NotNull(prefix);
                Assert.NotEmpty(prefix);
            }
        }        

        [Fact]
        public void TestRepositoryContainsDevelopBranch()
        {
            using (_testRepository = new Repository(_testPath))
            {
                Assert.NotNull(_testRepository.Branches[ _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Develop)]);                
            }
        }

        [Fact]
        public void StartNewFeatureCreatesNewBranch()
        {
            using (_testRepository = new Repository(_testPath))
            {
                var fullBranchname = _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Feature) + "testFeature";
                var newBranch = _testRepository.Branches[fullBranchname];
                if (newBranch!=null)
                    DeleteBranch(newBranch);

                Assert.Null(_testRepository.Branches[fullBranchname]);
                Assert.True(_testRepository.Flow().StartNewFeature("testFeature"));
                newBranch = _testRepository.Branches[fullBranchname];
                Assert.NotNull(newBranch);
                DeleteBranch(newBranch);
            }
        }

        internal void DeleteBranch(Branch branch)
        {
            if (branch == null)
                return;
            if (branch.IsCurrentRepositoryHead)
                _testRepository.Checkout(_testRepository.Branches[_testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Master)]);
            _testRepository.Branches.Remove(branch);
        }

        internal string CreateEmptyRepo(string dirName ="emptyRepo")
        {
            var path = Path.Combine(basepath, dirName);
            Directory.CreateDirectory(path);
            Thread.Sleep(1000);
            Repository.Init(path);
            return path;
        }

        internal void CleanRepoDir(string dirName = "emptyRepo")
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

        #region Implementation of IDisposable

        public void Dispose()
        {
            //CleanRepoDir();
        }

        #endregion
    }
}
