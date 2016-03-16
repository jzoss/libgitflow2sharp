using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public static class RepositoryHelpers
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="branchName"></param>
        /// <param name="branchTarget"></param>
        internal static Branch AddGetBranch(this Repository repository, string branchName,string branchTarget = "HEAD", bool track = true)
        {
            Branch branch;
            //Branch already has been created  
            if (!repository.TryGetLocalBranch(branchName, out branch))
            {
                if (!repository.TryGetRemoteBranch(branchName, out branch))
                {
                    //No branch made.. create branch
                    GitFlowExtensions.Log("Creating Branch : " + branchName);
                    branch = repository.CreateBranch(branchName,branchTarget);
                    if (track)
                    {
                        repository.SetRemoteBranch(branch);
                    }
                }
            }
            return branch;
        }

        internal static void SetRemoteBranch(this Repository repository, Branch branch, string remoteName = "origin")
        {
            Remote remote = repository.Network.Remotes[remoteName];
            repository.Branches.Update(branch,
                b => b.Remote = remote.Name,
                b => b.UpstreamBranch = branch.CanonicalName);
        }

        public static bool TryCheckout(this Repository repository, Branch branch, bool force = false)
        {
            CheckoutOptions options = new CheckoutOptions();
            if (force)
            {
                options.CheckoutModifiers = CheckoutModifiers.Force;
            }
            try
            {
                var checkoutBranch = repository.Checkout(branch, options);
                GitFlowExtensions.Log("Checked out Branch : " + checkoutBranch.FriendlyName);
            }
            catch (CheckoutConflictException conflict)
            {
                GitFlowExtensions.LogError("Checkout Failed", conflict.Message);
                return false;
            }
           
            return true;
        }

        public static bool TryCheckout(this Repository repository, string canonicalName, bool force = false)
        {
            var branch = repository.Branches.FirstOrDefault(
                        x => string.Equals(x.CanonicalName, canonicalName, StringComparison.OrdinalIgnoreCase));

            if (branch == null)
            {
                GitFlowExtensions.LogError("Branch not found ");
                return false;
            }
            return repository.TryCheckout(branch, force);

        }


        public static bool TryGetBranch(this Repository repository, string friendlyName, out Branch branch)
        {
            branch = repository.Branches.FirstOrDefault(
                        x => string.Equals(x.FriendlyName, friendlyName, StringComparison.OrdinalIgnoreCase));
            if (branch != null)
            {
                return true;
            }
            return false;
        }

        public static bool TryGetLocalBranch(this Repository repository, string friendlyName, out Branch branch)
        {
            branch = repository.Branches.Where(x => !x.IsRemote).FirstOrDefault(
                        x => string.Equals(x.FriendlyName, friendlyName, StringComparison.OrdinalIgnoreCase));
            if (branch != null)
            {
                return true;
            }
            return false;
        }

        public static bool TryGetRemoteBranch(this Repository repository, string friendlyName, out Branch branch)
        {
            branch = repository.Branches.Where(x => x.IsRemote).FirstOrDefault(
                        x => string.Equals(x.FriendlyName, friendlyName, StringComparison.OrdinalIgnoreCase));
            if (branch != null)
            {
                return true;
            }
            return false;
        }
    }
}
