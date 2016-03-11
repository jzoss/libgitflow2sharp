using System;
using System.ComponentModel.Design;
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
        readonly Signature author = new Signature("Test", "UnitTest@xunit.com", DateTime.Now);

        public InitTests()
        {
            _testPath = TestHelpers.CreateEmptyRepo(basepath);
        }

     
        [Fact]
        public void Init_FlowSetsConfigToDefaultValues()
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

        [Fact]
        public void Init_EmptyFlowCreatesBranches()
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

        [Theory]
        [InlineData("existingmaster","")]
        [InlineData("existingmaster", "existingdev")]
        [InlineData("", "existingdev")]
        public void Init_FlowWithExistingBranches(string masterBranch, string devBranch)
        {
            using (_testRepository = new Repository(_testPath))
            {
                var settings = new GitFlowRepoSettings();
                Signature committer = author;
                var opts = new CommitOptions { AllowEmptyCommit = true };
                _testRepository.Commit("Initial commit", author, committer, opts);
                TestHelpers.CreateLocalTestBranch(_testRepository, masterBranch, GitFlowSetting.Master, settings);
                TestHelpers.CreateLocalTestBranch(_testRepository, devBranch, GitFlowSetting.Develop, settings);
                _testRepository.Flow().Init(settings, author);
                Assert.NotNull(_testRepository.Branches.FirstOrDefault(
                        x => string.Equals(x.FriendlyName, settings.GetSetting(GitFlowSetting.Master), StringComparison.OrdinalIgnoreCase)));
                Assert.NotNull(_testRepository.Branches.FirstOrDefault(
                        x => string.Equals(x.FriendlyName, settings.GetSetting(GitFlowSetting.Develop), StringComparison.OrdinalIgnoreCase)));
            }
        }

        [Fact]
        public void Init_FlowIsInitialized()
        {
            using (_testRepository = new Repository(_testPath))
            {
                _testRepository.Flow().Init(new GitFlowRepoSettings(), author);
                Assert.True(_testRepository.Flow().IsInitialized());
            }
        }

        [Fact]
        public void Init_TestPrefixIsSet()
        {
            using (_testRepository = new Repository(_testPath))
            {
                _testRepository.Flow().Init(new GitFlowRepoSettings(), author);
                var prefix = _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Develop);
                Assert.NotNull(prefix);
                Assert.NotEmpty(prefix);
            }
        }        

        [Fact]
        public void Init_TestRepositoryContainsDevelopBranch()
        {
            using (_testRepository = new Repository(_testPath))
            {
                _testRepository.Flow().Init(new GitFlowRepoSettings(), author);
                Assert.NotNull(_testRepository.Branches[ _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Develop)]);                
            }
        }




        #region Implementation of IDisposable

        public void Dispose()
        {
            TestHelpers.CleanRepoDir(basepath);
        }

        #endregion
    }
}
