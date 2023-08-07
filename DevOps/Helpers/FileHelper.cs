using System.IO;

namespace DevOps.Helpers
{
    public static class FileHelper
    {
        public static string GetDeployConfigFileText() 
        {
            var configDirectory = PathHelper.ConfigDirectory;

            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }

            var customConfigPath = Properties.Settings.Default.CustomDeployConfigPath;
            var configPath = string.IsNullOrWhiteSpace(customConfigPath) ?
                            PathHelper.DeployConfigPath :
                            customConfigPath;

            if (!File.Exists(configPath)) 
            {
                var sourceConfigPath = PathHelper.TemplateDeployConfigPath;
                File.Copy(sourceConfigPath, configPath, true);
            }

            return File.ReadAllText(configPath);
        }

        public static void WriteDeployConfigToFile(string jsonStr) 
        {
            var customConfigPath = Properties.Settings.Default.CustomDeployConfigPath;
            var configPath = string.IsNullOrWhiteSpace(customConfigPath) ?
                                                        PathHelper.DeployConfigPath :
                                                        customConfigPath;
            File.WriteAllText(configPath, jsonStr);
        }
    }
}
