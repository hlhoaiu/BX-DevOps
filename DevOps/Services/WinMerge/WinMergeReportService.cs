using DevOps.Helpers;
using DevOps.Logger;
using System.IO;

namespace DevOps.Services.WinMerge
{
    public class WinMergeReportService : IWinMergeReportService
    {
        private readonly ILogger _logger;
        private readonly ICommandLineRunner _commandLineRunner;

        public WinMergeReportService(
            ILogger logger, 
            ICommandLineRunner commandLineRunner)
        {
            _logger = logger;
            _commandLineRunner = commandLineRunner;
        }

        public void GenerateReport(string path1, string path2, string reportOutputDirectory, string reportFileName)
        {
            var reportFullPath = Path.Combine(reportOutputDirectory, reportFileName);
            var command = @$"WinMergeU /r ""{path1}"" ""{path2}"" /or ""{reportFullPath}""";
            _commandLineRunner.Run(command, out var output, out var error, Properties.Settings.Default.WinMergeDirectory);
            if (File.Exists(reportFullPath))
            {
                _logger.Log($"[WinMergeReport] Successfully output report | TO: {reportFullPath}");
            }
        }
    }
}
