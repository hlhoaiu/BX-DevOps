using DevOps.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevOps.Models.Config
{
    public class DeployConfigModel : IDeployConfigModel
    {
        private readonly IRetrieveJSONConfigService _retrieveJSONConfigService;
        private readonly IUpdateJSONConfigService _updateJSONConfigService;

        private const string MASTER_STR = "master";

        private DeployConfig _deployConfig;
        private DeployJSONConfig _deployJSONConfig;

        public DeployConfigModel(
            IRetrieveJSONConfigService retrieveJSONConfigService, 
            IUpdateJSONConfigService updateJSONConfigService)
        {
            _retrieveJSONConfigService = retrieveJSONConfigService;
            _updateJSONConfigService = updateJSONConfigService;
        }

        public DeployConfig GetDeployConfig()
        {
            if (_deployConfig != null) return _deployConfig;

            var deployJSONConfig = GetDeployJSONConfig();
            // TODO: get latest hash from git
            _deployConfig = new DeployConfig(deployJSONConfig, "latest_hash");
            _deployConfig.SetTargetBranch(MASTER_STR);

            return _deployConfig;
        }

        public DeployJSONConfig GetDeployJSONConfig() 
        {
            if (_deployJSONConfig != null) return _deployJSONConfig;

            _deployJSONConfig = _retrieveJSONConfigService.Retrieve();

            return _deployJSONConfig;
        }

        public bool UpdateDeployConfig(DeployJSONConfig updatedConfig) 
        {
            var isUpdated = _updateJSONConfigService.Update(updatedConfig);
            if (!isUpdated) return false;
            _deployJSONConfig = updatedConfig;
            // TODO: get latest hash from git
            _deployConfig = new DeployConfig(updatedConfig, "latest_hash");
            _deployConfig.SetTargetBranch(MASTER_STR);
            // TODO: update JSON Config Serivce after i implemented it
            return true;
        }
    }
}
