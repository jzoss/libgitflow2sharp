using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsInitialized(this Flow gitflow)
        {
            var repo = gitflow.Repository;
            return repo.Config.Any(c => c.Key.StartsWith("gitflow.branch.master")) &&
                       repo.Config.Any(c => c.Key.StartsWith("gitflow.branch.develop"));
        }
    }
}
