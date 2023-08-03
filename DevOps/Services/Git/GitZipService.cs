using DevOps.Helpers;
using DevOps.Logger;
using DevOps.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.Git
{
    public class GitZipService : IGitZipService
    {
        private readonly ILogger _logger;

        public GitZipService(
            ILogger logger)
        {
            _logger = logger;
        }

        public void Zip(string fileFullPath, string gitHead, string gitDirectory)
        {
            var command = $"git archive --format zip --output \"{fileFullPath}\" {gitHead}";
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
