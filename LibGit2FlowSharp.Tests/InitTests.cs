using LibGit2FlowSharp.Enums;
using LibGit2Sharp;
using Xunit;

namespace LibGit2FlowSharp.Tests
{
    public class InitTests
    {
        private Repository _testRepository;
        private const string Testpath = @"C:\test\";

        public InitTests()
        {                        
        }


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
                //_testRepository.Flow()._testFlow = new Flow(new Repository(T));
            }
        }
    }
}
