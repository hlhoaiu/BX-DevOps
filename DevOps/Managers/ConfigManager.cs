using DevOps.Models.Config;

namespace DevOps.Managers
{
    public class ConfigManager : IConfigManager
    {
        private readonly IDeployConfigModel _deployConfigModel;

        public ConfigManager(
            IDeployConfigModel deployConfigModel)
        {
            _deployConfigModel = deployConfigModel;
        }

        public DeployConfig GetDeployConfig()
        {
            return _deployConfigModel.GetDeployConfig();
        }

        public DeployJSONConfig GetDeployJSONConfig()
        {
            return _deployConfigModel.GetDeployJSONConfig();
        }

        public bool Update(DeployJSONConfig deployJSONConfig)
        {
            return _deployConfigModel.UpdateDeployConfig(deployJSONConfig);
        }
    }
}
