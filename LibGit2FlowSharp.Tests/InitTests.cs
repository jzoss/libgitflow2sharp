using LibGit2FlowSharp.Attributes;
using LibGit2FlowSharp.Enums;
using LibGit2FlowSharp.Extensions;
using LibGit2Sharp;
using Xunit;

namespace LibGit2FlowSharp.Tests
{
    public class InitTests
    {
        private Repository _testRepository;
        private const string Testpath = @"C:\test\";
        
        [Fact]
        public void TestFolderIsAGitRepository() 
        {
            using (_testRepository = new Repository(Testpath))
            {
            }

        }

        [Fact]
        public void InitFlowSetsConfigToDefaultValues()
        {
            using (_testRepository = new Repository(Testpath))
            {
                _testRepository.Flow().Init(new GitFlowRepoSettings());
                
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

        [Fact]
        public void FlowIsInitialized()
        {
            using (_testRepository = new Repository(Testpath))
            {
                Assert.True(_testRepository.Flow().IsInitialized());
            }
        }

        [Fact]
        public void TestPrefixIsSet()
        {
            using (_testRepository = new Repository(Testpath))
            {
                var prefix = _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Develop);
                Assert.NotNull(prefix);
                Assert.NotEmpty(prefix);
            }
        }        

        [Fact]
        public void TestRepositoryContainsDevelopBranch()
        {
            using (_testRepository = new Repository(Testpath))
            {
                Assert.NotNull(_testRepository.Branches[ _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Develop)]);                
            }
        }

        [Fact]
        public void StartNewFeatureCreatesNewBranch()
        {
            using (_testRepository = new Repository(Testpath))
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
    }
}
