using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Xunit;
using LibGit2FlowSharp;

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
        public void InitFlowWithDefaultValues()
        {
            using (var repo = new Repository(Testpath))
            {
                repo.Flow().Init(new GitFlowRepoSettings());

                //TODO Finish 
                Assert.Equal(repo.Config.Get<string>("gitflow.branch.master").Value,"master");
                Assert.Equal(repo.Config.Get<string>("gitflow.branch.develop").Value, "develop");
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
                var prefix = _testRepository.Flow().GetPrefixByBranch(GitFlowExtensions.FlowBranch.Develop);
                Assert.NotNull(prefix);
                Assert.NotEmpty(prefix);
            }
        }
        [Fact]
        public void TestRepositoryContainsDevelopBranch()
        {
            using (_testRepository = new Repository(Testpath))
            {
                var branch = _testRepository.Branches[ _testRepository.Flow().GetPrefixByBranch(GitFlowExtensions.FlowBranch.Develop)];
                Assert.NotNull(branch);                
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
