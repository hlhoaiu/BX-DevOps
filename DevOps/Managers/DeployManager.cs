using DevOps.Models.Config;
using DevOps.Services.System;
using DevOps.Services.WinMerge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Managers
{
    public class DeployManager : IDeployManager
    {
        private readonly IWinMergeCompareService _winMergeCompareService;
        private readonly IDeployConfigModel _deployConfigModel;
        private readonly ICopyFileService _copyPackageService;

        public DeployManager(
            IWinMergeCompareService winMergeCompareService,
            IDeployConfigModel deployConfigModel,
            ICopyFileService copyPackageService)
        {
            _winMergeCompareService = winMergeCompareService;
            _deployConfigModel = deployConfigModel;
            _copyPackageService = copyPackageService;
        }

        public void DeployAndRelease()
        {
            var config = _deployConfigModel.GetDeployConfig();
            Directory.CreateDirectory(config.PackageReleasePath);
            var packagePath = Path.Combine(config.PackageBasePath, $"{config.PackageName}.zip");
            var releaseDirectories = new string[] { config.PackageReleasePath };
            _copyPackageService.Copy(packagePath, releaseDirectories);
            var formPath = Path.Combine(config.PackageBasePath, config.DeploymentFormName);
            _copyPackageService.Copy(formPath, releaseDirectories);
            return;
            _winMergeCompareService.Compare();
        }
    }
}
