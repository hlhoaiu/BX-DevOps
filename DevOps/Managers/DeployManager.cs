﻿using DevOps.Models.Config;
using DevOps.Services.System;
using DevOps.Services.WinMerge;
using System.IO;

namespace DevOps.Managers
{
    public class DeployManager : IDeployManager
    {
        private readonly IWinMergeCompareService _winMergeCompareService;
        private readonly IDeployConfigModel _deployConfigModel;
        private readonly ICopyFileService _copyFileService;
        private readonly IZipService _zipService;
        private readonly IOpenDirectoryService _openDirectoryService;

        public DeployManager(
            IWinMergeCompareService winMergeCompareService,
            IDeployConfigModel deployConfigModel,
            ICopyFileService copyPackageService,
            IZipService zipService,
            IOpenDirectoryService openDirectoryService)
        {
            _winMergeCompareService = winMergeCompareService;
            _deployConfigModel = deployConfigModel;
            _copyFileService = copyPackageService;
            _zipService = zipService;
            _openDirectoryService = openDirectoryService;
        }

        public void DeployAndRelease()
        {
            var config = _deployConfigModel.GetDeployConfig();
            ReleasePackage(config);
            Deploy(config);
        }

        private void ReleasePackage(DeployConfig config) 
        {
            Directory.CreateDirectory(config.PackageReleaseDirectory);
            var packagePath = Path.Combine(config.PackageWorkingDirectory, $"{config.PackageName}.zip");
            var releaseDirectories = new string[] { config.PackageReleaseDirectory };
            _copyFileService.Copy(packagePath, releaseDirectories);
            var formPath = Path.Combine(config.PackageWorkingDirectory, config.DeploymentFormName);
            _copyFileService.Copy(formPath, releaseDirectories);
            _openDirectoryService.Open(releaseDirectories);
        }

        private void Deploy(DeployConfig config) 
        {
            var sourceZipPath = Path.Combine(config.PackageCompiledDirectory, "Release.zip");
            var unZipToPath = Path.Combine(config.PackageCompiledDirectory, "Release");
            Directory.CreateDirectory(unZipToPath);
            _zipService.UnZip(sourceZipPath, unZipToPath);
            _winMergeCompareService.Compare(unZipToPath, config.ProductionProgramDirectory);
        }
    }
}
