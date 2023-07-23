using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DevOps.Models.Config
{
    public class DeployConfig
    {
        public DateTime CurrentDateTime { get; }
        public string ReleaseBranchName { get; } //release/yyyyMMdd
        public string TargetBranchName { get; private set; } //master or production
        public string TargetNugetPath { get; } // C:\ProgramSource\GenXls\packages
        public string PackageBasePath { get; } // C:\Users\itdxxx\Desktop
        public IEnumerable<string> JobIds { get; } = Enumerable.Empty<string>(); // xxxx+xxxx
        public string RepoLatestHash { get; }
        public string RepoPreviousMergeHash { get; }
        public string ProgramName { get; } // GenXls
        public string NugetRepoName { get; } //AS400.Interfaces
        public string PackageName { get; }// <ProgramName>.<CombinedJobIds>.<RepoLatestHash>.<CurrentDateTime>
        public string ProgramCompiledPath { get; } // C:\ProgramSource\GenXls\GenXls\bin\Debug
        public string PackageSourceZipFullPath { get; } // <PackageBasePath>\<PackageName>\source\<ProgramName>.<RepoLatestHash>.zip
        public string PackageSourceNugetZipFullPath { get; } // <PackageBasePath>\<PackageName>\source\<NugetRepoName>.<NugetPackVersion>.zip
        public string PackageCompilePath { get; } // <PackageBasePath>\<PackageName>\compiled
        public string PackageDiffPath { get; } // <PackageBasePath>\<PackageName>\diff
        public IEnumerable<string> CustomPackageBackUpPaths { get; } = Enumerable.Empty<string>();
        public string PackageBackUpPath { get; } // <PackageBackUpBasePath>\<ProgramName>\<CurrentDate>
        public string ProductionProgramBasePath { get; } // \\hkbcapp17p\e$\
        public string ProductionProgramPath { get; } // <ProductionProgramBasePath>\Apps\GENXLS
        public string ProductionBackUpBasePath { get; } // <ProductionProgramPath>\Migration\GenXls
        public string ProductionBackUpFullPath { get; } // <ProductionBackupBasePath>\<ProgramName>.<CurrentDateTime>.zip
        public string DiffHTMLName { get; } // <ProgramName>,<RepoPreviousMergeHash>_<RepoLatestHash>.html
        public string DeploymentFormName { get; } // <ProgramName>.<RepoLatestHash>.docx
        
        public DeployConfig(DeployJSONConfig deployConfig, string repoLatestHash) 
        {
            CurrentDateTime = DateTime.Now;
            ReleaseBranchName = deployConfig.ReleaseBranchName;
            TargetNugetPath = deployConfig.TargetNugetPath;
            PackageBasePath = deployConfig.PackageBasePath;
            JobIds = deployConfig.JobIds;
            RepoLatestHash = repoLatestHash;
            RepoPreviousMergeHash = deployConfig.RepoPreviousMergeHash;
            ProgramName = deployConfig.ProgramName;
            NugetRepoName = deployConfig.NugetRepoName;
            PackageName = $"{ProgramName}.{string.Join("+", JobIds)}";
            ProgramCompiledPath = deployConfig.ProgramCompiledPath;
            PackageSourceZipFullPath = deployConfig.PackageSourceZipFullPath;
            PackageSourceNugetZipFullPath = deployConfig.PackageSourceNugetZipFullPath;
            PackageCompilePath = deployConfig.PackageCompilePath;
            PackageDiffPath = deployConfig.PackageDiffPath;
            CustomPackageBackUpPaths = deployConfig.CustomPackageBackUpPaths;
            ProductionProgramBasePath = deployConfig.ProductionProgramBasePath;
            ProductionProgramPath = deployConfig.ProductionProgramPath;
            ProductionBackUpBasePath = deployConfig.ProductionBackUpBasePath;
            ProductionBackUpFullPath = deployConfig.ProductionBackUpFullPath;
            DiffHTMLName = deployConfig.DiffHTMLName;
            DeploymentFormName = deployConfig.DeploymentFormName;
        }

        public void SetTargetBranch(string targetBranch) 
        {
            TargetBranchName = targetBranch;
        }
    }
}
