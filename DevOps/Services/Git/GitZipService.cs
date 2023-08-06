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
            _logger.Log($"GitZip under Directory: {gitDirectory} | FROM: {gitHead} | TO: {fileFullPath}");
            var command = $"git archive --format zip --output \"{fileFullPath}\" {gitHead}";
            _commandLineRunner.Run(command, out var output, out var error, gitDirectory);
        }
    }
}
