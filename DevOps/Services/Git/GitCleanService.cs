using DevOps.Helpers;
using DevOps.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.Git
{
    public class GitCleanService : IGitCleanService
    {
        private readonly ILogger _logger;

        public GitCleanService(
            ILogger logger)
        {
            _logger = logger;
        }

        public void Clean(string gitDirectory)
        {
            var command = $"git clean -x -f -d";
            CommandLineRunner.Run(command, out var output, out var error, gitDirectory);
            if (!string.IsNullOrEmpty(output))
            {
                _logger.Log(output);
            }
            if (!string.IsNullOrEmpty(error))
            {
                _logger.Error(error);
            }
        }
    }
}
