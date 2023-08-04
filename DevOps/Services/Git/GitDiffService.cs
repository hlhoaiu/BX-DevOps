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

        public GitDiffService(ILogger logger)
        {
            _logger = logger;
        }

        public void Diff(string oldHash, string newHash, string gitDirectory)
        {
            var command = $"git difftool --dir-diff --tool winmerge {oldHash} {newHash}";
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
