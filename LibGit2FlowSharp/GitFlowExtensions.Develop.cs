using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsOnDevelopBranch(this Flow gitflow)
        {
            var repo = gitflow.Repository;

            if (!gitflow.IsInitialized())
                return false;
            var featurePrefix = repo.Config.Get<string>("gitflow.prefix.develop");
            if (featurePrefix == null)
                return false;
            return repo.Head.FriendlyName.StartsWith(featurePrefix.Value);
        }
    }
}
