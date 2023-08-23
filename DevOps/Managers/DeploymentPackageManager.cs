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
        private readonly IOpenDirectoryService _openDirectoryService;

        public DeploymentPackageManager(
            IGeneratePackageService generatePackageService,
            ICopyFileService copyPackageService,
            IDeployConfigModel deployConfigModel,
            IOpenDirectoryService openDirectoryService)
        {
            _generatePackageService = generatePackageService;
            _copyFileService = copyPackageService;
            _deployConfigModel = deployConfigModel;
            _openDirectoryService = openDirectoryService;
        }

        public void Generate()
        {
            var config = _deployConfigModel.GetDeployConfig();
            var packagePath = Path.Combine(config.PackageWorkingDirectory, $"{config.PackageName}.zip");
            if (!File.Exists(packagePath)) 
            {
                _generatePackageService.Generate();
                _openDirectoryService.Open(config.PackageWorkingDirectory);
            }
            var backupDirectories = config.CustomPackageBackUpDirectories;
            _copyFileService.Copy(packagePath, backupDirectories);
            _openDirectoryService.Open(backupDirectories);
        }
    }
}
