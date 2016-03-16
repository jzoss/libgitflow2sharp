using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2FlowSharp.Enums;
using LibGit2Sharp;
using Xunit;

namespace LibGit2FlowSharp.Tests
{

    [Collection("Repo collection")]
    public class FeatureTests : IDisposable
    {
        private Repository _testRepository;
        private string _testPath;
        Signature author = new Signature("Test", "UnitTest@xunit.com", DateTime.Now);
        private RepoFixture fixture;

        public FeatureTests(RepoFixture fixture)
        {
            this.fixture = fixture;
            _testPath = fixture.LocalRepoPath;
        }

        [Fact]
        public void Feature_StartNewFeatureCreatesNewBranch()
        {
            using (_testRepository = new Repository(_testPath))
            {
                var fullBranchname = _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Feature) + "testFeature";
                var newBranch = _testRepository.Branches[fullBranchname];
                if (newBranch != null)
                    TestHelpers.DeleteBranch(_testRepository, newBranch);

                Assert.Null(_testRepository.Branches[fullBranchname]);
                var info = _testRepository.Flow().StartNewFeature("testFeature");
                Assert.NotNull(info);
                newBranch = _testRepository.Branches[fullBranchname];
                Assert.NotNull(newBranch);
                //TestHelpers.DeleteBranch(_testRepository,newBranch);
            }
        }

        [Fact]
        public void Feature_PublishFeatureCreatesRemote()
        {
            using (_testRepository = new Repository(_testPath))
            {
                string branchName = TestHelpers.RandomString(8);
                var fullBranchname = _testRepository.Flow().GetPrefixByBranch(GitFlowSetting.Feature) + branchName;
                var newBranch = _testRepository.Branches[fullBranchname];
                if (newBranch != null)
                    TestHelpers.DeleteBranch(_testRepository, newBranch);

                var info = _testRepository.Flow().StartNewFeature(branchName);
                Assert.False(_testRepository.Branches[fullBranchname].IsTracking);
                _testRepository.Flow().PublishFeature(branchName);
                Assert.True(_testRepository.Branches[fullBranchname].IsTracking);
                //TestHelpers.DeleteBranch(_testRepository,newBranch);
            }
        }


        #region Implementation of IDisposable

        public void Dispose()
        {
            //TestHelpers.CleanRepoDir(basepath);
        }

        #endregion
    }
}
