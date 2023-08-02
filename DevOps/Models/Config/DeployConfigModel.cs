using DevOps.Services.Config;
using DevOps.Services.Git;
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
        private readonly IGitHashService _gitHashService;

        private DeployConfig _deployConfig;
        private DeployJSONConfig _deployJSONConfig;

        public DeployConfigModel(
            IRetrieveJSONConfigService retrieveJSONConfigService,
            IUpdateJSONConfigService updateJSONConfigService,
            IGitHashService gitHashService)
        {
            _retrieveJSONConfigService = retrieveJSONConfigService;
            _updateJSONConfigService = updateJSONConfigService;
            _gitHashService = gitHashService;
        }

        public DeployConfig GetDeployConfig()
        {
            if (_deployConfig != null) return _deployConfig;

            var deployJSONConfig = GetDeployJSONConfig();
            var latestHash = _gitHashService.GetHash(CommonConst.Production, deployJSONConfig.ProgramGitPath);
            _deployConfig = new DeployConfig(deployJSONConfig, latestHash);

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
            var latestHash = _gitHashService.GetHash(CommonConst.Production, _deployJSONConfig.ProgramGitPath);
            _deployConfig = new DeployConfig(updatedConfig, latestHash);
            return true;
        }
    }
}
