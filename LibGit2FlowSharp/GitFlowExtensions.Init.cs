using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {

        public static void Init(this Flow gitflow, GitFlowRepoSettings settings)
        {
            var repo = gitflow.Repository;


            //Only init if it is not initialized 
            //TODO figure out what to do if it is :)
            if(!gitflow.IsInitialized())
            {
                if(repo.Branches.Count() <= 0)
                {
                    foreach (var item in settings.Settings)
                    {
                        var key =  GetConfigKey(item.Key);
                        repo.Config.Set(key, item.Value,settings.ConfigurationLocation);
                    }
                }

                //TODO -- Figure out this crazyness 
                else
                {

                }
            }

        }


        //private void SetConfigValue(Repository repository, ) 

        public static bool IsInitialized(this Flow gitflow)
        {
            var repo = gitflow.Repository;
            return repo.Config.Any(c => c.Key.StartsWith("gitflow.branch.master")) &&
                       repo.Config.Any(c => c.Key.StartsWith("gitflow.branch.develop"));
        }
    }
	
}
