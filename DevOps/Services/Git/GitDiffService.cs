using DevOps.Helpers;
using DevOps.Logger;
using System.IO;

namespace DevOps.Services.Git
{
    public class GitDiffService : IGitDiffService
    {
        private readonly ILogger _logger;
        private readonly ICommandLineRunner _commandLineRunner;

        public GitDiffService(
            ILogger logger, 
            ICommandLineRunner commandLineRunner)
        {
            _logger = logger;
            _commandLineRunner = commandLineRunner;
        }

        public void Diff(string oldHash, string newHash, string gitDirectory, bool isGenDiffReport)
        {
            var diffToolName = isGenDiffReport ? "winmergereport" : "winmerge";
            // defined in H:\.gitconfig and will generate to Properties.Settings.Default.GitDiffReportWorkingPath
            var command = $"git difftool --dir-diff --tool {diffToolName} {oldHash} {newHash}";
            _commandLineRunner.Run(command, out var output, out var error, gitDirectory);
            if (isGenDiffReport) 
            {
                if (File.Exists(Properties.Settings.Default.GitDiffReportWorkingPath))
                {
                    _logger.Log($"[DIFFREPORT] Successfully generate diff report | TO: {Properties.Settings.Default.GitDiffReportWorkingPath}");
                }
                // and the report diff details folder
            }
        }
    }
}
