using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public class GitFlow
    {
        internal Repository Repository { get; private set; }
        public GitFlow(Repository repository)
        {
            Repository = repository;
        }
    }
}
