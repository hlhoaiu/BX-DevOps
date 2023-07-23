using DevOps.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Models.Config
{
    public class DeployConfigModel : IDeployConfigModel
    {
        private readonly IRetrieveJSONConfigService _retrieveJSONConfigService;

        private const string MASTER_STR = "master";

        private DeployConfig _deployConfig;

        public DeployConfigModel(
            IRetrieveJSONConfigService retrieveJSONConfigService)
        {
            _retrieveJSONConfigService = retrieveJSONConfigService;
        }

        public DeployConfig GetDeployConfig()
        {
            if (_deployConfig != null) return _deployConfig;

            var jsonConfig = _retrieveJSONConfigService.Retrieve();
            // TODO: get latest hash from git
            _deployConfig = new DeployConfig(jsonConfig, "latest_hash");
            _deployConfig.SetTargetBranch(MASTER_STR);

            return _deployConfig;
        }

        public DeployConfig UpdateDeployConfig(DeployJSONConfig updatedConfig) 
        {
            // TODO: get latest hash from git
            _deployConfig = new DeployConfig(updatedConfig, "latest_hash");
            _deployConfig.SetTargetBranch(MASTER_STR);
            // TODO: update JSON Config Serivce after i implemented it
            return _deployConfig;
        }
    }
}
