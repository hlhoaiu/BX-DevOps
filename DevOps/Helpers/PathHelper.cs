using System;
using System.IO;

namespace DevOps.Helpers
{
    public static class PathHelper
    {
        public static string CurrentDirectory => Directory.GetParent(Environment.CurrentDirectory).FullName;
        public static string ProjectDirectory => Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public static string ConfigDirectory => Path.Combine(CurrentDirectory, "Configs");
        public static string DeployConfigPath => Path.Combine(ConfigDirectory, Properties.Settings.Default.DeployConfigFileName);
        public static string TemplateDeployConfigPath => Path.Combine(ProjectDirectory, "Configs", Properties.Settings.Default.DeployConfigFileName);
    }
}
