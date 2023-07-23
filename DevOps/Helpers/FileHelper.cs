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
            var customConfigPath = Properties.Settings.Default.CustomDeployConfigPath;
            var sourceConfigPath = string.IsNullOrWhiteSpace(customConfigPath) ?
                                    PathHelper.TemplateDeployConfigPath :
                                    customConfigPath;
            File.Copy(sourceConfigPath, configPath, true);

            return File.ReadAllText(configPath);
        }

        public static void WriteDeployConfigToFile(string jsonStr) 
        {
            var configPath = PathHelper.DeployConfigPath;
            File.WriteAllText(configPath, jsonStr);
        }
    }
}
