using DevOps.Logger;
using System.Collections.Generic;
using System.IO;

namespace DevOps.Services.System
{
    public class CopyFileService : ICopyFileService
    {
        private readonly ILogger _logger;

        public CopyFileService(
            ILogger logger)
        {
            _logger = logger;
        }

        public void Copy(string filePath, IEnumerable<string> targetDirectories) 
        {
            var fileName = Path.GetFileName(filePath);
            foreach (var directory in targetDirectories)
            {
                Directory.CreateDirectory(directory);
                File.Copy(filePath, Path.Combine(directory, fileName), true);
            }
        }
    }
}
