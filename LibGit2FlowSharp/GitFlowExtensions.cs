using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public enum FlowBranch
        {
            Master, Develop, Feature, HotFix, Release, Production, Support
        }
        public static Flow Flow(this Repository repository)
        {
            return new Flow(repository);
        }
    }
}
