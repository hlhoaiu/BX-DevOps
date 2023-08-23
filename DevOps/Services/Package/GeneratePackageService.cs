using DevOps.Logger;
using DevOps.Models;
using DevOps.Models.Config;
using DevOps.Services.Git;
using System.IO;

namespace DevOps.Services.System
{
    public class GeneratePackageService : IGeneratePackageService
    {
        private readonly IDeployConfigModel _deployConfigModel;
        private readonly IGitZipService _gitZipService;
        private readonly IZipService _zipService;
        private readonly IGitDiffService _gitDiffService;
        private readonly ILogger _logger;
        private readonly IMoveFileService _moveFileService;
        private readonly IMoveDirectoryService _moveDirectoryService;

        public GeneratePackageService(
            IDeployConfigModel deployConfigModel,
            IGitZipService gitZipService,
            IZipService zipService,
            IGitDiffService gitDiffService,
            ILogger logger,
            IMoveFileService moveFileService, 
            IMoveDirectoryService moveDirectoryService)
        {
            _deployConfigModel = deployConfigModel;
            _gitZipService = gitZipService;
            _zipService = zipService;
            _gitDiffService = gitDiffService;
            _logger = logger;
            _moveFileService = moveFileService;
            _moveDirectoryService = moveDirectoryService;
        }

        public void Generate()
        {
            var config = _deployConfigModel.GetDeployConfig();
            CreateFolder(config);
            ZipSourceFolder(config);
            GenerateWinMergeReport(config.RepoPreviousMergeHash, config.ProgramGitDirectory, config.PackageDiffDirectory);
            CompileProgram();
            ZipCompiledProgram(config.ProgramCompiledDirectory, config.PackageCompiledDirectory);
            ZipWholePackage(config.PackageWorkingDirectory, config.PackageName);
        }

        private void CreateFolder(DeployConfig config) 
        {
            _logger.Log("Start to prepare package folders");
            Directory.CreateDirectory(config.PackageCompiledDirectory);
            Directory.CreateDirectory(config.PackageDiffDirectory);
            Directory.CreateDirectory(Path.GetDirectoryName(config.PackageSourceZipPath));
        }

        private void ZipSourceFolder(DeployConfig config) 
        {
            _logger.Log($"Start to zip source folder into package.");
            var fileFullPath = config.PackageSourceZipPath;
            var gitHead = CommonConst.Production;
            var gitDirectory = config.ProgramGitDirectory;
            _gitZipService.Zip(fileFullPath, gitHead, gitDirectory);

            foreach (var nugetConfig in config.NugetConfigs)
            {
                if (nugetConfig.IsEmpty) continue;
                fileFullPath = nugetConfig.PackageSourceNugetZipPath;
                gitHead = CommonConst.Master;
                gitDirectory = nugetConfig.NugetGitDirectory;
                _gitZipService.Zip(fileFullPath, gitHead, gitDirectory);
            }
        }

        private void GenerateWinMergeReport(string oldHash, string gitDirectory, string packageDiffPath) 
        {
            _logger.Log($"Start to generate WinMerge report into package");
            var newHash = CommonConst.Production;
            var isGenDiffReport = true;
            _gitDiffService.Diff(oldHash, newHash, gitDirectory, isGenDiffReport);
            var diffReportOutputPath = Properties.Settings.Default.GitDiffReportWorkingPath;
            var diffReportDirectory = Path.GetDirectoryName(diffReportOutputPath);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(diffReportOutputPath);
            var diffDetailsFolderName = $"{fileNameWithoutExtension}.files";
            _moveFileService.Move(diffReportOutputPath, packageDiffPath);
            _moveDirectoryService.Move(Path.Combine(diffReportDirectory, diffDetailsFolderName), Path.Combine(packageDiffPath, diffDetailsFolderName));
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
