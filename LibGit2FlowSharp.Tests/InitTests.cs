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
        public void Test1()
        {
            using (var repo = new Repository("c:\test"))
            {

            }
        }
    }
}
