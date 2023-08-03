using DevOps.Models.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.Package
{
    public class MovePackageService : IMovePackageService
    {
        private readonly IDeployConfigModel _deployConfigModel;

        public MovePackageService(
            IDeployConfigModel deployConfigModel)
        {
            _deployConfigModel = deployConfigModel;
        }

        public void Move()
        {
            var config = _deployConfigModel.GetDeployConfig();
            var packageName = $"{config.PackageName}.zip";
            var packagePath = Path.Combine(config.PackageBasePath, packageName);
            var targetPath = Path.Combine(config.PackageBackUpPath, packageName);
            Directory.CreateDirectory(config.PackageBackUpPath);
            File.Copy(packagePath, targetPath);
            foreach (var path in config.CustomPackageBackUpPaths)
            {
                Directory.CreateDirectory(path);
                File.Copy(packagePath, Path.Combine(path, packageName));
            }
        }
    }
}
