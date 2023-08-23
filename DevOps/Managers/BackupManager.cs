using DevOps.Models.Config;
using DevOps.Services.System;
using System.IO;

namespace DevOps.Managers
{
    public class BackupManager : IBackupManager
    {
        private readonly IDeployConfigModel _deployConfigModel;
        private readonly IZipService _zipService;
        private readonly IOpenDirectoryService _openDirectoryService;

        public BackupManager(
            IDeployConfigModel deployConfigModel,
            IZipService zipService,
            IOpenDirectoryService openDirectoryService)
        {
            _deployConfigModel = deployConfigModel;
            _zipService = zipService;
            _openDirectoryService = openDirectoryService;
        }

        public void Backup()
        {
            var config = _deployConfigModel.GetDeployConfig();
            var sourceFolder = config.ProductionProgramDirectory;
            var zipPath = config.ProductionBackUpZipPath;
            _zipService.Zip(sourceFolder, zipPath);
            var zipDirectory = Path.GetDirectoryName(zipPath);
            _openDirectoryService.Open(zipDirectory);
        }
    }
}
