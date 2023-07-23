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

            var configPath = PathHelper.DeployConfigPath;

            if (!File.Exists(configPath)) 
            {
                var customConfigPath = Properties.Settings.Default.CustomDeployConfigPath;
                var sourceConfigPath = string.IsNullOrWhiteSpace(customConfigPath) ? 
                                        PathHelper.TemplateDeployConfigPath : 
                                        customConfigPath;
                File.Copy(sourceConfigPath, configPath);
            }

            return File.ReadAllText(configPath);
        }
    }
}
