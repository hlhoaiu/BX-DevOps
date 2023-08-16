using DevOps.Helpers;
using DevOps.Logger;

namespace DevOps.Services.WinMerge
{
    public class WinMergeCompareService : IWinMergeCompareService
    {
        private readonly ILogger _logger;
        private readonly ICommandLineRunner _commandLineRunner;

        public WinMergeCompareService(
            ILogger logger,
            ICommandLineRunner commandLineRunner)
        {
            _logger = logger;
            _commandLineRunner = commandLineRunner;
        }

        public void Compare(string path1, string path2)
        {
            var command = @$"WinMergeU /r ""{path1}"" ""{path2}""";
            _commandLineRunner.Run(command, out var output, out var error, Properties.Settings.Default.WinMergeDirectory);
        }
    }
}
