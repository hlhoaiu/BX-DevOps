using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DevOps.Models.Config
{
    public class DeployConfig
    {
        public DateTime CurrentDateTime { get; }
        public string ReleaseBranchName { get; } //release/yyyyMMdd
        public string ProgramGitDirectory { get; }
        public string PackageWorkingDirectory { get; } // C:\Users\itdxxx\Desktop
        public IEnumerable<string> JobIds { get; } = Enumerable.Empty<string>(); // xxxx+xxxx
        public string RepoLatestHash { get; }
        public string RepoPreviousMergeHash { get; }
        public string ProgramName { get; } // GenXls
        public string PackageName { get; }// <ProgramName>.<CombinedJobIds>.<RepoLatestHash>.<CurrentDateTime>
        public string ProgramCompiledDirectory { get; } // C:\ProgramSource\GenXls\GenXls\bin\Debug
        public string PackageSourceZipPath { get; } // <PackageBasePath>\<PackageName>\source\<ProgramName>.<RepoLatestHash>.zip
        public string PackageCompiledDirectory { get; } // <PackageBasePath>\<PackageName>\compiled
        public string PackageDiffDirectory { get; } // <PackageBasePath>\<PackageName>\diff
        public IEnumerable<string> CustomPackageBackUpDirectories { get; } = Enumerable.Empty<string>();
        public string ProductionDirectory { get; } // \\hkbcapp17p\e$\
        public string PackageReleaseDirectory { get; } // <ProductionProgramPath>\Release\<ProgramName>\<CurrentDate>
        public string ProductionProgramDirectory { get; } // <ProductionProgramBasePath>\Apps\GENXLS
        public string ProductionBackUpDirectory { get; } // <ProductionProgramPath>\Migration\GenXls
        public string ProductionBackUpZipPath { get; } // <ProductionBackupBasePath>\<ProgramName>.<CurrentDateTime>.zip
        public string DiffHTMLName { get; } // <ProgramName>,<RepoPreviousMergeHash>_<RepoLatestHash>.html
        public string DeploymentFormName { get; } // <ProgramName>.<RepoLatestHash>.docx
        public IEnumerable<NugetConfig> NugetConfigs { get; set; } = Enumerable.Empty<NugetConfig>();

        public DeployConfig(DeployJSONConfig deployConfig, string repoLatestHash, DateTime initDateTime) 
        {
            CurrentDateTime = initDateTime;
            ReleaseBranchName = deployConfig.ReleaseBranchName;
            ProgramGitDirectory = deployConfig.ProgramGitDirectory;
            PackageWorkingDirectory = deployConfig.PackageWorkingDirectory;
            JobIds = deployConfig.JobIds;
            RepoLatestHash = repoLatestHash;
            RepoPreviousMergeHash = deployConfig.RepoPreviousMergeHash;
            ProgramName = deployConfig.ProgramName;
            PackageName = $"{ProgramName}.{string.Join("+", JobIds)}.{repoLatestHash}.{CurrentDateTime.ToString(CommonConst.DateTimeFormat)}";
            ProgramCompiledDirectory = deployConfig.ProgramCompiledDirectory;
            PackageSourceZipPath = Path.Combine(PackageWorkingDirectory, PackageName, "source", $"{ProgramName}.{RepoLatestHash}.zip");
            PackageCompiledDirectory = Path.Combine(PackageWorkingDirectory, PackageName, "compiled");
            PackageDiffDirectory = Path.Combine(PackageWorkingDirectory, PackageName, "diff");
            CustomPackageBackUpDirectories = deployConfig.CustomPackageBackUpDirectories;
            ProductionDirectory = deployConfig.ProductionDirectory;
            PackageReleaseDirectory = Path.Combine(ProductionDirectory, "Release", ProgramName, CurrentDateTime.ToString(CommonConst.DateFormat));
            ProductionProgramDirectory = Path.Combine(ProductionDirectory, "Apps", ProgramName.ToUpper());
            ProductionBackUpDirectory = Path.Combine(ProductionDirectory, "Migration", ProgramName);
            ProductionBackUpZipPath = Path.Combine(ProductionBackUpDirectory, $"{ProgramName.ToUpper()}.{CurrentDateTime.ToString(CommonConst.DateTimeFormat)}.zip");
            DiffHTMLName = $"{ProgramName},{RepoPreviousMergeHash}_{RepoLatestHash}";
            DeploymentFormName = $"{ProgramName}.{RepoLatestHash}.docx";
            NugetConfigs = deployConfig.NugetConfigs.Select(x => new NugetConfig(x, Path.Combine(PackageWorkingDirectory, PackageName, "source", $"{x.NugetPackageName}.{x.NugetPackageVersion}.zip")));
        }

        public DeployJSONConfig ToJSONConfig() 
        {
            return new DeployJSONConfig
            {
                ReleaseBranchName = ReleaseBranchName,
                ProgramGitDirectory = ProgramGitDirectory,
                PackageWorkingDirectory = PackageWorkingDirectory,
                JobIds = JobIds,
                RepoPreviousMergeHash = RepoPreviousMergeHash,
                ProgramName = ProgramName,
                ProgramCompiledDirectory = ProgramCompiledDirectory,
                CustomPackageBackUpDirectories = CustomPackageBackUpDirectories,
                ProductionDirectory = ProductionDirectory,
                NugetConfigs = NugetConfigs.Select(x => x.ToJSONConfig())
            };
        }
    }

    public class NugetConfig
    {
        public string NugetGitDirectory { get; }
        public string ProgramNugetPackageDirectory { get; } // C:\ProgramSource\GenXls\packages
        public string NugetPackageName { get; } //AS400.Interfaces
        public string NugetPackageVersion { get; } //v1.11.1
        public string PackageSourceNugetZipPath { get; } // <PackageBasePath>\<PackageName>\source\<NugetRepoName>.<NugetPackVersion>.zip
        public bool IsEmpty =>
            string.IsNullOrWhiteSpace(NugetGitDirectory) ||
            string.IsNullOrWhiteSpace(ProgramNugetPackageDirectory) ||
            string.IsNullOrWhiteSpace(NugetPackageName) ||
            string.IsNullOrWhiteSpace(NugetPackageVersion);

        public NugetConfig(NugetJSONConfig jsonConfig, string packageSourceNugetZipFullPath) 
        {
            NugetGitDirectory = jsonConfig.NugetGitDirectory;
            ProgramNugetPackageDirectory = jsonConfig.ProgramNugetPackageDirectory;
            NugetPackageName = jsonConfig.NugetPackageName;
            NugetPackageVersion = jsonConfig.NugetPackageVersion;
            PackageSourceNugetZipPath = packageSourceNugetZipFullPath;
        }

        public NugetJSONConfig ToJSONConfig()
        {
            return new NugetJSONConfig
            {
                NugetGitDirectory = NugetGitDirectory,
                ProgramNugetPackageDirectory = ProgramNugetPackageDirectory,
                NugetPackageName = NugetPackageName,
                NugetPackageVersion = NugetPackageVersion
            };
        }
    }
}
