using DevOps.Helpers;
using DevOps.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.Git
{
    public class GitHashService : IGitHashService
    {
        private readonly ILogger _logger;
        private readonly ICommandLineRunner _commandLineRunner;

        public GitHashService(
            ILogger logger, 
            ICommandLineRunner commandLineRunner)
        {
            _logger = logger;
            _commandLineRunner = commandLineRunner;
        }

        public string GetHash(string branch, string gitDirectory)
        {
            //var command = $"git rev-parse --short {branch}";
            var command = @"git log -1 --format="" % h";
            _commandLineRunner.Run(command, out var output, out var error, gitDirectory);
            if (string.IsNullOrEmpty(output)) 
            {
                throw new ArgumentNullException($"Git hash should never been null or empty.");
            }
            return output.Trim();
        }
    }
}
