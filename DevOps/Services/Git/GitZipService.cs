using DevOps.Helpers;
using DevOps.Logger;
using System.IO;

namespace DevOps.Services.Git
{
    public class GitZipService : IGitZipService
    {
        private readonly ILogger _logger;
        private readonly ICommandLineRunner _commandLineRunner;

        public GitZipService(
            ILogger logger, 
            ICommandLineRunner commandLineRunner)
        {
            _logger = logger;
            _commandLineRunner = commandLineRunner;
        }

        public void Zip(string fileFullPath, string gitHead, string gitDirectory)
        {
            var command = $"git archive --format zip --output \"{fileFullPath}\" {gitHead}";
            _commandLineRunner.Run(command, out var output, out var error, gitDirectory);
            if (File.Exists(fileFullPath)) 
            {
                _logger.Log($"[GITZIP] Successfully zip | TO: {fileFullPath}");
            }
        }
    }
}
