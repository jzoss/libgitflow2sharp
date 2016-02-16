using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsOnReleaseBranch(this Flow gitflow)
        {
            var repo = gitflow.Repository;

            if (!gitflow.IsInitialized())
                return false;
            var featurePrefix = repo.Config.Get<string>("gitflow.prefix.release");
            if (featurePrefix == null)
                return false;
            return repo.Head.FriendlyName.StartsWith(featurePrefix.Value);
        }
    }
}
