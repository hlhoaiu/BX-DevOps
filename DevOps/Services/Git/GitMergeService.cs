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

        public GitMergeService(
            ILogger logger)
        {
            _logger = logger;
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
}
