using DevOps.Models.Config;
using DevOps.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DeployConfig Retrieve()
        {
            return _deployConfigModel.GetDeployConfig();
        }

        public DeployConfig Modify()
        {
            var updatedConfig = new DeployJSONConfig(); // TODO: get updated value from UI
            return _deployConfigModel.UpdateDeployConfig(updatedConfig);
        }
    }
}
