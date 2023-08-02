﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace DevOps.Models.Config
{
    public class DeployConfig
    {
        public DateTime CurrentDateTime { get; }
        public string ReleaseBranchName { get; } //release/yyyyMMdd
        public string ProgramGitPath { get; }
        public string TargetNugetPath { get; } // C:\ProgramSource\GenXls\packages
        public string PackageBasePath { get; } // C:\Users\itdxxx\Desktop
        public IEnumerable<string> JobIds { get; } = Enumerable.Empty<string>(); // xxxx+xxxx
        public string RepoLatestHash { get; }
        public string RepoPreviousMergeHash { get; }
        public string ProgramName { get; } // GenXls
        public string NugetRepoName { get; } //AS400.Interfaces
        public string NugetPackageVersion { get; } //v1.11.1
        public string PackageName { get; }// <ProgramName>.<CombinedJobIds>.<RepoLatestHash>.<CurrentDateTime>
        public string ProgramCompiledPath { get; } // C:\ProgramSource\GenXls\GenXls\bin\Debug
        public string PackageSourceZipFullPath { get; } // <PackageBasePath>\<PackageName>\source\<ProgramName>.<RepoLatestHash>.zip
        public string PackageSourceNugetZipFullPath { get; } // <PackageBasePath>\<PackageName>\source\<NugetRepoName>.<NugetPackVersion>.zip
        public string PackageCompilePath { get; } // <PackageBasePath>\<PackageName>\compiled
        public string PackageDiffPath { get; } // <PackageBasePath>\<PackageName>\diff
        public IEnumerable<string> CustomPackageBackUpPaths { get; } = Enumerable.Empty<string>();
        public string ProductionProgramBasePath { get; } // \\hkbcapp17p\e$\
        public string PackageBackUpPath { get; } // <ProductionProgramPath>\Release\<ProgramName>\<CurrentDate>
        public string ProductionProgramPath { get; } // <ProductionProgramBasePath>\Apps\GENXLS
        public string ProductionBackUpBasePath { get; } // <ProductionProgramPath>\Migration\GenXls
        public string ProductionBackUpFullPath { get; } // <ProductionBackupBasePath>\<ProgramName>.<CurrentDateTime>.zip
        public string DiffHTMLName { get; } // <ProgramName>,<RepoPreviousMergeHash>_<RepoLatestHash>.html
        public string DeploymentFormName { get; } // <ProgramName>.<RepoLatestHash>.docx
        
        public DeployConfig(DeployJSONConfig deployConfig, string repoLatestHash) 
        {
            CurrentDateTime = DateTime.Now;
            ReleaseBranchName = deployConfig.ReleaseBranchName;
            ProgramGitPath = deployConfig.ProgramGitPath;
            TargetNugetPath = deployConfig.TargetNugetPath;
            PackageBasePath = deployConfig.PackageBasePath;
            JobIds = deployConfig.JobIds;
            RepoLatestHash = repoLatestHash;
            RepoPreviousMergeHash = deployConfig.RepoPreviousMergeHash;
            ProgramName = deployConfig.ProgramName;
            NugetRepoName = deployConfig.NugetRepoName;
            NugetPackageVersion = deployConfig.NugetPackageVersion;
            PackageName = $"{ProgramName}.{string.Join("+", JobIds)}.{repoLatestHash}.{CurrentDateTime.ToString(CommonConst.DateTimeFormat)}";
            ProgramCompiledPath = deployConfig.ProgramCompiledPath;
            PackageSourceZipFullPath = Path.Combine(PackageBasePath, PackageName, "source", $"{ProgramName}.{RepoLatestHash}.zip");
            PackageSourceNugetZipFullPath = Path.Combine(PackageBasePath, PackageName, "source", $"{NugetRepoName}.{NugetPackageVersion}.zip");
            PackageCompilePath = Path.Combine(PackageBasePath, PackageName, "compiled");
            PackageDiffPath = $@"{PackageBasePath}\{PackageName}\diff";
            PackageDiffPath = Path.Combine(PackageBasePath, PackageName, "diff");
            CustomPackageBackUpPaths = deployConfig.CustomPackageBackUpPaths;
            ProductionProgramBasePath = deployConfig.ProductionProgramBasePath;
            PackageBackUpPath = Path.Combine(ProductionProgramBasePath, "Release", "GenXls", CurrentDateTime.ToString(CommonConst.DateFormat));
            ProductionProgramPath = Path.Combine(ProductionProgramBasePath, "Apps", "GENXLS");
            ProductionBackUpBasePath = Path.Combine(ProductionProgramBasePath, "Migration", "GenXls");
            ProductionBackUpFullPath = Path.Combine(ProductionProgramBasePath, "Release", ProgramName, CurrentDateTime.ToString(CommonConst.DateTimeFormat));
            DiffHTMLName = $"{ProgramName},{RepoPreviousMergeHash}_{RepoLatestHash}";
            DeploymentFormName = $"{ProgramName}.{RepoLatestHash}.docx";
        }

        public DeployJSONConfig ToJSONConfig() 
        {
            return new DeployJSONConfig
            {
                ReleaseBranchName = ReleaseBranchName,
                ProgramGitPath = ProgramGitPath,
                TargetNugetPath = TargetNugetPath,
                PackageBasePath = PackageBasePath,
                JobIds = JobIds,
                RepoPreviousMergeHash = RepoPreviousMergeHash,
                ProgramName = ProgramName,
                NugetRepoName = NugetRepoName,
                NugetPackageVersion = NugetPackageVersion,
                ProgramCompiledPath = ProgramCompiledPath,
                CustomPackageBackUpPaths = CustomPackageBackUpPaths,
                ProductionProgramBasePath = ProductionProgramBasePath
            };
        }
    }
}
