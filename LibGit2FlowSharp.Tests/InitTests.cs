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
        private Flow _testFlow;
        private const string Testpath = @"c:\test";

        public InitTests()
        {   
            // could do the          
        }

        [Fact]
        public void TestFolderIsAGitRepository() 
        {
            using (var repo = new Repository(Testpath))
            {
            }
        }

        [Fact]
        public void FlowSetsRepository()
        {
            _testFlow = new Flow(new Repository(Testpath));
            Assert.NotNull(_testFlow.Repository);            
        }

        [Fact]
        public void FlowIsInitialized()
        {
            _testFlow = new Flow(new Repository(Testpath));
            Assert.True(_testFlow.IsInitialized());
        }
    }
}
