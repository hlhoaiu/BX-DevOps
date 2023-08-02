using DevOps.Models.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.Package
{
    public class GeneratePackageService : IGeneratePackageService
    {
        private readonly IDeployConfigModel _deployConfigModel;

        public GeneratePackageService(
            IDeployConfigModel deployConfigModel)
        {
            _deployConfigModel = deployConfigModel;
        }

        public void Generate()
        {
            var config = _deployConfigModel.GetDeployConfig();
            Directory.CreateDirectory(config.PackageCompilePath);
            Directory.CreateDirectory(config.PackageDiffPath);
            Directory.CreateDirectory(Path.GetDirectoryName(config.PackageSourceNugetZipFullPath));
        }
    }
}
