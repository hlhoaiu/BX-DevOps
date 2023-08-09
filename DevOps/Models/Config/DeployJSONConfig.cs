using System.Collections.Generic;
using System.Linq;

namespace DevOps.Models.Config
{
    public class DeployJSONConfig
    {
        public string ReleaseBranchName { get; set; } //release/yyyyMMdd
        public string ProgramGitPath { get; set; }
        public string PackageBasePath { get; set; } // C:\Users\itdxxx\Desktop
        public IEnumerable<string> JobIds { get; set; } = Enumerable.Empty<string>(); // xxxx+xxxx
        public string RepoPreviousMergeHash { get; set; }
        public string ProgramName { get; set; } // GenXls
        public string ProgramCompiledPath { get; set; } // C:\ProgramSource\GenXls\GenXls\bin\Release
        public IEnumerable<string> CustomPackageBackUpPaths { get; set; } = Enumerable.Empty<string>();
        public string ProductionProgramBasePath { get; set; } // \\hkbcapp17p\e$\
        public IEnumerable<NugetJSONConfig> NugetConfigs { get; set; } = Enumerable.Empty<NugetJSONConfig>();

        public DeployJSONConfig() { }

        public DeployJSONConfig(IDictionary<string, string> fieldDict) 
        {
            var tempStr = string.Empty;
            ReleaseBranchName = fieldDict.TryGetValue(nameof(ReleaseBranchName), out tempStr) ? tempStr : "";
            PackageBasePath = fieldDict.TryGetValue(nameof(PackageBasePath), out tempStr) ? tempStr : "";
            JobIds = fieldDict.TryGetValue(nameof(JobIds), out tempStr) ? tempStr.Split('+') : Enumerable.Empty<string>();
            RepoPreviousMergeHash = fieldDict.TryGetValue(nameof(RepoPreviousMergeHash), out tempStr) ? tempStr : ""; ;
            ProgramName = fieldDict.TryGetValue(nameof(ProgramName), out tempStr) ? tempStr : "";
            ProgramCompiledPath = fieldDict.TryGetValue(nameof(ProgramCompiledPath), out tempStr) ? tempStr : "";
            CustomPackageBackUpPaths = fieldDict.TryGetValue(nameof(CustomPackageBackUpPaths), out tempStr) ? tempStr.Split(',') : Enumerable.Empty<string>();
            ProductionProgramBasePath = fieldDict.TryGetValue(nameof(ProductionProgramBasePath), out tempStr) ? tempStr : "";
            ProgramGitPath = fieldDict.TryGetValue(nameof(ProgramGitPath), out tempStr) ? tempStr : "";
            var allNugetFields = fieldDict.Where(x => x.Key.Contains("Nuget")).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            NugetConfigs = GatherNugetConfig(allNugetFields);
        }

        private IEnumerable<NugetJSONConfig> GatherNugetConfig(IDictionary<string, string> nugetFields) 
        {
            var totalCount = nugetFields.Where(x=>x.Key.Contains('_')).Select(x => x.Key.Split('_')[1]).Distinct().Count();
            var configList = new List<NugetJSONConfig>();
            for (int i = 0; i < totalCount; i++)
            {
                var nugetConfig = new NugetJSONConfig(nugetFields, i);
                configList.Add(nugetConfig);
            }
            return configList;
        }
    }

    public class NugetJSONConfig 
    {
        public string NugetGitPath { get; set; }
        public string TargetNugetPath { get; set; } // C:\ProgramSource\GenXls\packages
        public string NugetRepoName { get; set; } //AS400.Interfaces
        public string NugetPackageVersion { get; set; } //v1.11.1

        public NugetJSONConfig() { }

        public NugetJSONConfig(IDictionary<string, string> fieldDict, int index)
        {
            var tempStr = string.Empty;
            NugetGitPath = fieldDict.TryGetValue($"{nameof(NugetGitPath)}_{index}", out tempStr) ? tempStr : "";
            TargetNugetPath = fieldDict.TryGetValue($"{nameof(TargetNugetPath)}_{index}", out tempStr) ? tempStr : "";
            NugetRepoName = fieldDict.TryGetValue($"{nameof(NugetRepoName)}_{index}", out tempStr) ? tempStr : "";
            NugetPackageVersion = fieldDict.TryGetValue($"{nameof(NugetPackageVersion)}_{index}", out tempStr) ? tempStr : "";
        }
    }
}
