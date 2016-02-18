using LibGit2FlowSharp.Enums;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void InitSettings()
        {
            foreach (GitFlowSetting setting in Enum.GetValues(typeof(GitFlowSetting)))
            {
                Settings.Add(setting, setting.GetAttribute<GitFlowConfigAttribute>().DefaultValue);
            }
        }
    }
}
