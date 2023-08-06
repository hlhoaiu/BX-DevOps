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
                var targetPath = Path.Combine(directory, fileName);
                _logger.Log($"[COPY] File attempt to copy FROM: {filePath} | TO: {targetPath}");
                File.Copy(filePath, targetPath, true);
                if (File.Exists(targetPath))
                {
                    _logger.Log($"[COPY] File successfully copy to {targetPath}");
                }
            }
        }
    }
}
