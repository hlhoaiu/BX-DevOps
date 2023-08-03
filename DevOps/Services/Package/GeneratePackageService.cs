using DevOps.Models;
using DevOps.Models.Config;
using DevOps.Services.Git;
using DevOps.Services.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.Package
{
    public class GeneratePackageService : IGeneratePackageService
    {
        private readonly IDeployConfigModel _deployConfigModel;
        private readonly IGitZipService _gitZipService;
        private readonly IZipService _zipService;

        public GeneratePackageService(
            IDeployConfigModel deployConfigModel,
            IGitZipService gitZipService,
            IZipService zipService)
        {
            _deployConfigModel = deployConfigModel;
            _gitZipService = gitZipService;
            _zipService = zipService;
        }

        public void Generate()
        {
            var config = _deployConfigModel.GetDeployConfig();
            CreateFolder(config);
            ZipReleaseFolder(config);
            GenerateWinMergeReport();
            CompileProgram();
            ZipCompiledProgram(config);
            ZipWholePackage(config);
        }

        private void CreateFolder(DeployConfig config) 
        {
            Directory.CreateDirectory(config.PackageCompilePath);
            Directory.CreateDirectory(config.PackageDiffPath);
            Directory.CreateDirectory(Path.GetDirectoryName(config.PackageSourceNugetZipFullPath));
        }

        private void ZipReleaseFolder(DeployConfig config) 
        {
            var fileFullPath = config.PackageSourceZipFullPath;
            var gitHead = CommonConst.Production;
            var gitDirectory = config.ProgramGitPath;
            _gitZipService.Zip(fileFullPath, gitHead, gitDirectory);

            // TODO: Loop More than one Nuget Package
            fileFullPath = config.PackageSourceNugetZipFullPath;
            gitHead = CommonConst.Master;
            gitDirectory = config.ProgramGitPath;
            _gitZipService.Zip(fileFullPath, gitHead, gitDirectory);
        }

        private void GenerateWinMergeReport() 
        {
            
        }

        private void CompileProgram() 
        {
            // Considering do it manually during merge branch stage
        }

        private void ZipCompiledProgram(DeployConfig config)
        {
            var sourceFolder = config.ProgramCompiledPath;
            var zipPath = Path.Combine(config.PackageCompilePath, "Release.zip");
            _zipService.Zip(sourceFolder, zipPath);
        }

        private void ZipWholePackage(DeployConfig config) 
        {
            var sourceFolder = Path.Combine(config.PackageBasePath, config.PackageName);
            var zipPath = Path.Combine(config.PackageBasePath, $"{config.PackageName}.zip");
            _zipService.Zip(sourceFolder, zipPath);
        }
    }
}
