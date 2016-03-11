using LibGit2FlowSharp.Enums;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using LibGit2FlowSharp.Attributes;
using LibGit2FlowSharp.Extensions;

namespace LibGit2FlowSharp
{
    public class GitFlowRepoSettings
    {
        public Dictionary<GitFlowSetting, string> Settings { get; private set; }

        public ConfigurationLevel ConfigurationLocation { get; set; }

        public GitFlowRepoSettings()
        {
            Settings = new Dictionary<GitFlowSetting, string>();
            ConfigurationLocation = ConfigurationLevel.Local;
            InitSettings();
        }

        public string GetSetting(GitFlowSetting setting)
        {
            return Settings.ContainsKey(setting) ? Settings[setting] : null;
        }

        public void SetSetting(GitFlowSetting setting, string settingValue)
        {
            Settings[setting] = settingValue;
        }

        private void InitSettings()
        {
            foreach (GitFlowSetting setting in Enum.GetValues(typeof(GitFlowSetting)))
            {
                Settings.Add(setting, setting.GetAttribute<GitFlowConfigAttribute>().DefaultValue);
            }
        }
    }
}
