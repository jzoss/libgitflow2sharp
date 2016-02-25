using System;

namespace LibGit2FlowSharp.Attributes
{
    public class GitFlowConfigAttribute : Attribute
    {
        internal GitFlowConfigAttribute(string configName, string defaultValue, string friendlyName = "")
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
