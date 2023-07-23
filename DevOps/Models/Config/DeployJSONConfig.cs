using System.Collections.Generic;

namespace DevOps.Models.Config
{
    public class DeployJSONConfig
    {
        public string ReleaseBranchName { get; set; } //release/yyyyMMdd
        public string TargetNugetPath { get; set; } // C:\ProgramSource\GenXls\packages
        public string PackageBasePath { get; set; } // C:\Users\itdxxx\Desktop
        public IEnumerable<string> JobIds { get; set; } // xxxx+xxxx
        public string RepoPreviousMergeHash { get; set; }
        public string ProgramName { get; set; } // GenXls
        public string NugetRepoName { get; set; } //AS400.Interfaces
        public string ProgramCompiledPath { get; set; } // C:\ProgramSource\GenXls\GenXls\bin\Debug
        public string PackageSourceZipFullPath { get; set; } // <PackageBasePath>\<PackageName>\source\<ProgramName>.<RepoLatestHash>.zip
        public string PackageSourceNugetZipFullPath { get; set; } // <PackageBasePath>\<PackageName>\source\<NugetRepoName>.<NugetPackVersion>.zip
        public string PackageCompilePath { get; set; } // <PackageBasePath>\<PackageName>\compiled
        public string PackageDiffPath { get; set; } // <PackageBasePath>\<PackageName>\diff
        public IEnumerable<string> CustomPackageBackUpPaths { get; set; }
        public string PackageBackUpPath { get; set; } // <PackageBackUpBasePath>\<ProgramName>\<CurrentDate>
        public string ProductionProgramBasePath { get; set; } // \\hkbcapp17p\e$\
        public string ProductionProgramPath { get; set; } // <ProductionProgramBasePath>\Apps\GENXLS
        public string ProductionBackUpBasePath { get; set; } // <ProductionProgramPath>\Migration\GenXls
        public string ProductionBackUpFullPath { get; set; } // <ProductionBackupBasePath>\<ProgramName>.<CurrentDateTime>.zip
        public string DiffHTMLName { get; set; } // <ProgramName>,<RepoPreviousMergeHash>_<RepoLatestHash>.html
        public string DeploymentFormName { get; set; } // <ProgramName>.<RepoLatestHash>.docx
    }
}
