using DevOps.Helpers;
using DevOps.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Diff(string oldHash, string newHash, string gitDirectory)
        {
            var command = $"git difftool --dir-diff --tool winmerge {oldHash} {newHash}";
            _commandLineRunner.Run(command, out var output, out var error, gitDirectory);
        }
    }
}
