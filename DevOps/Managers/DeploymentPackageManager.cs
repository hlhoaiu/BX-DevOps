using DevOps.Models.Config;
using DevOps.Services.System;
using System.IO;

namespace DevOps.Managers
{
    public class DeploymentPackageManager : IDeploymentPackageManager
    {
        private readonly IGeneratePackageService _generatePackageService;
        private readonly ICopyFileService _copyFileService;
        private readonly IDeployConfigModel _deployConfigModel;

        public DeploymentPackageManager(
            IGeneratePackageService generatePackageService,
            ICopyFileService copyPackageService,
            IDeployConfigModel deployConfigModel)
        {
            _generatePackageService = generatePackageService;
            _copyFileService = copyPackageService;
            _deployConfigModel = deployConfigModel;
        }

        public void Generate()
        {
            var config = _deployConfigModel.GetDeployConfig();
            var packagePath = Path.Combine(config.PackageBasePath, $"{config.PackageName}.zip");
            if (!File.Exists(packagePath)) 
            {
                _generatePackageService.Generate();
            }
            var backupDirectories = config.CustomPackageBackUpPaths;
            _copyFileService.Copy(packagePath, backupDirectories);
        }
    }
}
