using DevOps.Logger;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            foreach (var directory in targetDirectories.Where(x=>!string.IsNullOrWhiteSpace(x)))
            {
                Directory.CreateDirectory(directory);
                var targetPath = Path.Combine(directory, fileName);
                _logger.Log($"[COPY] File attempt to copy FROM: {filePath} | TO: {targetPath}");
                try
                {
                    File.Copy(filePath, targetPath, true);
                }
                catch (global::System.Exception ex)
                {
                    _logger.Error($"[COPY] FROM: {filePath} | TO: {targetPath} | {ex}");
                }
                if (File.Exists(targetPath))
                {
                    _logger.Log($"[COPY] File successfully copy to {targetPath}");
                }
            }
        }
    }
}
