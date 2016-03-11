using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public class BranchInfo
    {

        public string FriendlyName { get; set; }
        public string Author { get; set; }
        public DateTimeOffset LastCommit { get; set; }
        public bool IsTracking { get; set; }
        public bool IsCurrentBranch { get; set; }
        public bool IsRemote { get; set; }
        public string CanonicalName { get; set; }
        public string CommitId { get; set; }
        public string Message { get; set; }

        public string Prefix { get; set; }


        public string Name
        {
            get { return FriendlyName.Replace(Prefix, ""); }
        }

        public BranchInfo()
        {
            
        }

        public BranchInfo(Branch branch, string prefix)
        {
            Prefix = prefix;
            Author = branch.Tip.Author.Name;
            CanonicalName = branch.CanonicalName;
            FriendlyName = branch.FriendlyName;
            LastCommit = branch.Tip.Author.When;
            IsTracking = branch.IsTracking;
            IsCurrentBranch = branch.IsCurrentRepositoryHead;
            IsRemote = branch.IsRemote;
            CommitId = branch.Tip.Id.ToString();
            Message = branch.Tip.MessageShort;
        }
    }
}
