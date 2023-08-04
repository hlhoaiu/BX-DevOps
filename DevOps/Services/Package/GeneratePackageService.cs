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

        public GeneratePackageService(
            IDeployConfigModel deployConfigModel,
            IGitZipService gitZipService,
            IZipService zipService,
            IGitDiffService gitDiffService)
        {
            _deployConfigModel = deployConfigModel;
            _gitZipService = gitZipService;
            _zipService = zipService;
            _gitDiffService = gitDiffService;
        }

        public void Generate()
        {
            var config = _deployConfigModel.GetDeployConfig();
            CreateFolder(config);
            ZipReleaseFolder(config);
            GenerateWinMergeReport(config.RepoPreviousMergeHash, config.ProgramGitPath);
            CompileProgram();
            ZipCompiledProgram(config.ProgramCompiledPath);
            ZipWholePackage(config.PackageBasePath, config.PackageName);
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
            gitDirectory = config.NugetGitPath;
            _gitZipService.Zip(fileFullPath, gitHead, gitDirectory);
        }

        private void GenerateWinMergeReport(string oldHash, string gitDirectory) 
        {
            _gitDiffService.Diff(oldHash, CommonConst.Production, gitDirectory);
        }

        private void CompileProgram() 
        {
            // Considering do it manually during merge branch stage
        }

        private void ZipCompiledProgram(string programCompiledDirectory)
        {
            var sourceFolder = programCompiledDirectory;
            var zipPath = Path.Combine(programCompiledDirectory, "Release.zip");
            _zipService.Zip(sourceFolder, zipPath);
        }

        private void ZipWholePackage(string packageDirectory, string packageName) 
        {
            var sourceFolder = Path.Combine(packageDirectory, packageName);
            var zipPath = Path.Combine(packageDirectory, $"{packageName}.zip");
            _zipService.Zip(sourceFolder, zipPath);
        }
    }
}
