using System.Collections.Generic;
using System.Linq;

namespace DevOps.Models.Config
{
    public class DeployJSONConfig
    {
        public string ReleaseBranchName { get; set; } //release/yyyyMMdd
        public string ProgramGitPath { get; set; }
        public string TargetNugetPath { get; set; } // C:\ProgramSource\GenXls\packages
        public string PackageBasePath { get; set; } // C:\Users\itdxxx\Desktop
        public IEnumerable<string> JobIds { get; set; } // xxxx+xxxx
        public string RepoPreviousMergeHash { get; set; }
        public string ProgramName { get; set; } // GenXls
        public string NugetRepoName { get; set; } //AS400.Interfaces
        public string NugetPackageVersion { get; set; } //v1.11.1
        public string ProgramCompiledPath { get; set; } // C:\ProgramSource\GenXls\GenXls\bin\Release
        public IEnumerable<string> CustomPackageBackUpPaths { get; set; }
        public string ProductionProgramBasePath { get; set; } // \\hkbcapp17p\e$\
    }
}
