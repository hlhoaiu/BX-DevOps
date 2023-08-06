using DevOps.Logger;
using DevOps.Models.Config;
using DevOps.Services.System;
using DevOps.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Managers
{
    public class BackupManager : IBackupManager
    {
        private readonly IDeployConfigModel _deployConfigModel;
        private readonly IZipService _zipService;

        public BackupManager(
            IDeployConfigModel deployConfigModel,
            IZipService zipService)
        {
            _deployConfigModel = deployConfigModel;
            _zipService = zipService;
        }

        public void Backup()
        {
            var config = _deployConfigModel.GetDeployConfig();
            var sourceFolder = config.ProductionProgramPath;
            var zipPath = config.ProductionBackUpFullPath;
            _zipService.Zip(sourceFolder, zipPath);
        }
    }
}
