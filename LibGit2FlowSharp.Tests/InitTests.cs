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

        [Fact]
        public void FolderIsGitRepository()
        {
            using (var repo = new Repository(@"c:\test"))
            {

            }
        }
    }
}
