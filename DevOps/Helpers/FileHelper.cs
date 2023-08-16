using Microsoft.Office.Interop.Word;
using System.IO;

namespace DevOps.Helpers
{
    public static class FileHelper
    {
        public static string GetDeployConfigFileText() 
        {
            var configDirectory = PathProvider.ConfigDirectory;

            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }

            var customConfigPath = Properties.Settings.Default.CustomDeployConfigPath;
            var configPath = string.IsNullOrWhiteSpace(customConfigPath) ?
                            PathProvider.DeployConfigPath :
                            customConfigPath;

            if (!File.Exists(configPath)) 
            {
                var sourceConfigPath = PathProvider.TemplateDeployConfigPath;
                File.Copy(sourceConfigPath, configPath, true);
            }

            return File.ReadAllText(configPath);
        }

        public static void WriteDeployConfigToFile(string jsonStr) 
        {
            var customConfigPath = Properties.Settings.Default.CustomDeployConfigPath;
            var configPath = string.IsNullOrWhiteSpace(customConfigPath) ?
                                                        PathProvider.DeployConfigPath :
                                                        customConfigPath;
            File.WriteAllText(configPath, jsonStr);
        }

        public static void DeleteCachedConfigFile() 
        {
            File.Delete(PathProvider.DeployConfigPath);
        }

        public static Document GetImplFormDocument(Application wordApp) 
        {
            var templateDirectory = PathProvider.TemplateDirectory;

            if (!Directory.Exists(templateDirectory))
            {
                Directory.CreateDirectory(templateDirectory);
            }

            var implFormPath = PathProvider.ImplFormPath;
            var sourceConfigPath = PathProvider.TemplateImplFormPath;
            File.Copy(sourceConfigPath, implFormPath, true);

            return wordApp.Documents.Open(implFormPath, ReadOnly: false, Visible: true);
        }
    }
}
