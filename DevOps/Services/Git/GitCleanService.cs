using DevOps.Helpers;
using DevOps.Logger;

namespace DevOps.Services.Git
{
    public class GitCleanService : IGitCleanService
    {
        private readonly ICommandLineRunner _commandLineRunner;
        private readonly ILogger _logger;

        public GitCleanService(
            ILogger logger, 
            ICommandLineRunner commandLineRunner)
        {
            _logger = logger;
            _commandLineRunner = commandLineRunner;
        }

        public void Clean(string gitDirectory)
        {
            var command = $"git clean -x -f -d";
            _commandLineRunner.Run(command, out var output, out var error, gitDirectory);
        }
    }
}
