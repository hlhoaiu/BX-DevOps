using DevOps.Models.Config;
using DevOps.Services.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Managers
{
    public class DeploymentPackageManager : IDeploymentPackageManager
    {
        private readonly IGeneratePackageService _generatePackageService;
        private readonly ICopyFileService _copyPackageService;
        private readonly IDeployConfigModel _deployConfigModel;

        public DeploymentPackageManager(
            IGeneratePackageService generatePackageService,
            ICopyFileService copyPackageService,
            IDeployConfigModel deployConfigModel)
        {
            _generatePackageService = generatePackageService;
            _copyPackageService = copyPackageService;
            _deployConfigModel = deployConfigModel;
        }

        public void Release()
        {
            var config = _deployConfigModel.GetDeployConfig();
            var packagePath = Path.Combine(config.PackageBasePath, $"{config.PackageName}.zip");
            if (!File.Exists(packagePath)) 
            {
                _generatePackageService.Generate();
            }
            var backupDirectories = new string[] { config.PackageBackUpPath }.Concat(config.CustomPackageBackUpPaths);
            _copyPackageService.Copy(packagePath, backupDirectories);
        }
    }
}
