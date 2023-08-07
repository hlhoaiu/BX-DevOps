using System.Collections.Generic;
using System.Linq;

namespace DevOps.Models.Config
{
    public class DeployJSONConfig
    {
        public string ReleaseBranchName { get; set; } //release/yyyyMMdd
        public string ProgramGitPath { get; set; }
        public string NugetGitPath { get; set; }
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

        public DeployJSONConfig() { }

        public DeployJSONConfig(IDictionary<string, string> fieldDict) 
        {
            var tempStr = string.Empty;
            ReleaseBranchName = fieldDict.TryGetValue(nameof(ReleaseBranchName), out tempStr) ? tempStr : "";
            TargetNugetPath = fieldDict.TryGetValue(nameof(TargetNugetPath), out tempStr) ? tempStr : "";
            PackageBasePath = fieldDict.TryGetValue(nameof(PackageBasePath), out tempStr) ? tempStr : "";
            JobIds = fieldDict.TryGetValue(nameof(JobIds), out tempStr) ? tempStr.Split('+') : Enumerable.Empty<string>();
            RepoPreviousMergeHash = fieldDict.TryGetValue(nameof(RepoPreviousMergeHash), out tempStr) ? tempStr : ""; ;
            ProgramName = fieldDict.TryGetValue(nameof(ProgramName), out tempStr) ? tempStr : "";
            NugetRepoName = fieldDict.TryGetValue(nameof(NugetRepoName), out tempStr) ? tempStr : "";
            ProgramCompiledPath = fieldDict.TryGetValue(nameof(ProgramCompiledPath), out tempStr) ? tempStr : "";
            CustomPackageBackUpPaths = fieldDict.TryGetValue(nameof(CustomPackageBackUpPaths), out tempStr) ? tempStr.Split(',') : Enumerable.Empty<string>();
            ProductionProgramBasePath = fieldDict.TryGetValue(nameof(ProductionProgramBasePath), out tempStr) ? tempStr : "";
            ProgramGitPath = fieldDict.TryGetValue(nameof(ProgramGitPath), out tempStr) ? tempStr : "";
            NugetGitPath = fieldDict.TryGetValue(nameof(NugetGitPath), out tempStr) ? tempStr : "";
            NugetPackageVersion = fieldDict.TryGetValue(nameof(NugetPackageVersion), out tempStr) ? tempStr : "";
        }
    }
}
