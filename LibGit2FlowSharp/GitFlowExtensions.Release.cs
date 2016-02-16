﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static bool IsOnReleaseBranch(this Flow gitFlow)
        {
            return IsOnSpecifiedBranch(gitFlow, FlowBranch.Release);
        }
    }
}
