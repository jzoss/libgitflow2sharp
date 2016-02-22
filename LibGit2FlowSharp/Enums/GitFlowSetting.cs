using System;

namespace LibGit2FlowSharp.Enums
{
    public enum GitFlowSetting
    {
        [GitFlowConfig("gitflow.branch.master", "master", "Master branch")]
        Master,
        [GitFlowConfig("gitflow.branch.develop", "develop", "Development branch")]
        Develop,
        [GitFlowConfig("gitflow.prefix.feature", "feature/", "Feature branch prefix")]
        Feature,
        [GitFlowConfig("gitflow.prefix.bugfix", "bugfix/", "Bugfix branch prefix")]
        BugFix,
        [GitFlowConfig("gitflow.prefix.hotfix", "hotfix/", "Hotfix branch prefix")]
        HotFix,
        [GitFlowConfig("gitflow.prefix.release", "release/","Release branch prefix")]
        Release,
        [GitFlowConfig("gitflow.prefix.support", "support/","Support branch prefix")]
        Support,
        [GitFlowConfig("gitflow.prefix.versiontag", "")]
        VersionTag
    }


    public class GitFlowConfigAttribute : Attribute
    {
        internal GitFlowConfigAttribute(string configName,string defaultValue ,string friendlyName ="")
        {
            ConfigName = configName;
            DefaultValue = defaultValue;
            FriendlyName = friendlyName;
        }
        public string ConfigName { get; private set; }
        public string DefaultValue { get; private set; }
        public string FriendlyName { get; private set; }
    }

}
