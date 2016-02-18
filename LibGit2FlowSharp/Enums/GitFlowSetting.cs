using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGit2FlowSharp.Enums
{
    public enum GitFlowSetting
    {
        [GitFlowConfig("gitflow.branch.master", "master", "Master Branch")]
        Master,
        [GitFlowConfig("gitflow.branch.develop", "develop")]
        Develop,
        [GitFlowConfig("gitflow.prefix.feature", "feature/")]
        Feature,
        [GitFlowConfig("gitflow.prefix.bugfix", "bugfix/")]
        BugFix,
        [GitFlowConfig("gitflow.prefix.hotfix", "hotfix/")]
        HotFix,
        [GitFlowConfig("gitflow.prefix.release", "hotfix/")]
        Release,
        [GitFlowConfig("gitflow.prefix.support", "support/")]
        Support,
        [GitFlowConfig("gitflow.prefix.versiontag", "")]
        VersionTag
    }


    public class GitFlowConfigAttribute : Attribute
    {
        internal GitFlowConfigAttribute(string configName,string defaultValue ,string friendlyName ="")
        {
            this.ConfigName = configName;
            this.DefaultValue = defaultValue;
            this.FriendlyName = friendlyName;
        }
        public string ConfigName { get; private set; }
        public string DefaultValue { get; private set; }
        public string FriendlyName { get; private set; }
    }

}
