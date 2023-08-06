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
    public class GitMergeService : IGitMergeService
    {
        private readonly ILogger _logger;
        private readonly ICommandLineRunner _commandLineRunner;

        public GitMergeService(
            ILogger logger, 
            ICommandLineRunner commandLineRunner)
        {
            _logger = logger;
            _commandLineRunner = commandLineRunner;
        }

        public void Merge(string sourceBranch, string mergeBranch, string gitDirectory)
        {
            var commands = new string[] 
            {
                $"git checkout {sourceBranch}",
                $"git merge {mergeBranch}",
                $"git push --all"
            };
            foreach (var command in commands)
            {
                _commandLineRunner.Run(command, out var output, out var error, gitDirectory);
            }
        }
    }
}
