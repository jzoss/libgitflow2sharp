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
    public class FeatureTests : IDisposable
    {
        private Repository _testRepository;
        private const string basepath = @"C:\test\";
        private string _testPath;
        Signature author = new Signature("Test", "UnitTest@xunit.com", DateTime.Now);

        public FeatureTests()
        {
            _testPath = TestHelpers.CreateEmptyRepo(basepath);
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
                Assert.True(_testRepository.Flow().StartNewFeature("testFeature"));
                newBranch = _testRepository.Branches[fullBranchname];
                Assert.NotNull(newBranch);
                TestHelpers.DeleteBranch(_testRepository,newBranch);
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
