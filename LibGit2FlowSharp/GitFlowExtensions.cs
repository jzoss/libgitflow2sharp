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
        public static GitFlow GitFlow(this Repository repository)
        {
            return new GitFlow(repository);
        }
    }
}
