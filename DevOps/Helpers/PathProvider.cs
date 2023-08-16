using System;
using System.IO;

namespace DevOps.Helpers
{
    public static class PathProvider
    {
        public static string LogDirectory => string.IsNullOrEmpty(Properties.Settings.Default.LogDirectory) ? Path.Combine(CurrentDirectory, "Logs") : Properties.Settings.Default.LogDirectory;
        public static string CurrentDirectory => Directory.GetParent(Environment.CurrentDirectory).FullName;
        public static string ProjectDirectory => Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public static string ConfigDirectory => Path.Combine(CurrentDirectory, "Configs");
        public static string DeployConfigPath => Path.Combine(ConfigDirectory, Properties.Settings.Default.DeployConfigFileName);
        public static string TemplateDeployConfigPath => Path.Combine(ProjectDirectory, "Configs", Properties.Settings.Default.DeployConfigFileName);
        public static string TemplateImplFormPath => Path.Combine(ProjectDirectory, "Template", Properties.Settings.Default.ImplFormFileName);
        public static string TemplateDirectory => Path.Combine(CurrentDirectory, "Templates");
        public static string ImplFormPath => Path.Combine(ConfigDirectory, Properties.Settings.Default.ImplFormFileName);
    }
}
