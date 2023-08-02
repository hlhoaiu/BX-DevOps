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

        public GitHashService(ILogger logger)
        {
            _logger = logger;
        }

        public string GetHash(string branch, string gitDirectory)
        {
            var command = $"git rev-parse --short {branch}";
            CommandLineRunner.Run(command, out var output, out var error, gitDirectory);
            if (!string.IsNullOrEmpty(error)) 
            {
                var errorMsg = $"Git hash error: {error}";
                _logger.Error(errorMsg);
                throw new Exception($"Git hash error: {errorMsg}");
            }
            return output.Trim();
        }
    }
}
