using DevOps.Logger;
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

namespace DevOps.Services.System
{
    public class GeneratePackageService : IGeneratePackageService
    {
        private readonly IDeployConfigModel _deployConfigModel;
        private readonly IGitZipService _gitZipService;
        private readonly IZipService _zipService;
        private readonly IGitDiffService _gitDiffService;
        private readonly ILogger _logger;

        public GeneratePackageService(
            IDeployConfigModel deployConfigModel,
            IGitZipService gitZipService,
            IZipService zipService,
            IGitDiffService gitDiffService,
            ILogger logger)
        {
            _deployConfigModel = deployConfigModel;
            _gitZipService = gitZipService;
            _zipService = zipService;
            _gitDiffService = gitDiffService;
            _logger = logger;
        }

        public void Generate()
        {
            var config = _deployConfigModel.GetDeployConfig();
            CreateFolder(config);
            ZipSourceFolder(config);
            GenerateWinMergeReport(config.RepoPreviousMergeHash, config.ProgramGitPath);
            CompileProgram();
            ZipCompiledProgram(config.ProgramCompiledPath, config.PackageCompilePath);
            ZipWholePackage(config.PackageBasePath, config.PackageName);
        }

        private void CreateFolder(DeployConfig config) 
        {
            _logger.Log("Start to prepare package folders");
            Directory.CreateDirectory(config.PackageCompilePath);
            Directory.CreateDirectory(config.PackageDiffPath);
            Directory.CreateDirectory(Path.GetDirectoryName(config.PackageSourceZipFullPath));
        }

        private void ZipSourceFolder(DeployConfig config) 
        {
            _logger.Log($"Start to zip source folder into package.");
            var fileFullPath = config.PackageSourceZipFullPath;
            var gitHead = CommonConst.Production;
            var gitDirectory = config.ProgramGitPath;
            _gitZipService.Zip(fileFullPath, gitHead, gitDirectory);

            foreach (var nugetConfig in config.NugetConfigs)
            {
                if (nugetConfig.IsEmpty) continue;
                fileFullPath = nugetConfig.PackageSourceNugetZipFullPath;
                gitHead = CommonConst.Master;
                gitDirectory = nugetConfig.NugetGitPath;
                _gitZipService.Zip(fileFullPath, gitHead, gitDirectory);
            }
        }

        private void GenerateWinMergeReport(string oldHash, string gitDirectory) 
        {
            _logger.Log($"Start to generate WinMerge report into package");
            var newHash = CommonConst.Production;
            _gitDiffService.Diff(oldHash, newHash, gitDirectory);
        }

        private void CompileProgram() 
        {
            // Considering do it manually during merge branch stage
        }

        private void ZipCompiledProgram(string programCompiledDirectory, string targetDirectory)
        {
            _logger.Log($"Start to zip compiled release folder into package");
            var sourceFolder = programCompiledDirectory;
            var zipPath = Path.Combine(targetDirectory, "Release.zip");
            _zipService.Zip(sourceFolder, zipPath);
        }

        private void ZipWholePackage(string packageDirectory, string packageName) 
        {
            _logger.Log($"Start to zip whole package folder");
            var sourceFolder = Path.Combine(packageDirectory, packageName);
            var zipPath = Path.Combine(packageDirectory, $"{packageName}.zip");
            _zipService.Zip(sourceFolder, zipPath);
        }
    }
}
