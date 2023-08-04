using DevOps.Logger;
using System.Collections.Generic;
using System.IO;

namespace DevOps.Services.System
{
    public class MoveFileService : IMoveFileService
    {
        private readonly ILogger _logger;

        public MoveFileService(
            ILogger logger)
        {
            _logger = logger;
        }

        public void Move(string filePath, string targetDirectory) 
        {
            var fileName = Path.GetFileName(filePath);
            Directory.CreateDirectory(targetDirectory);
            File.Move(filePath, Path.Combine(targetDirectory, fileName), true);
        }
    }
}
