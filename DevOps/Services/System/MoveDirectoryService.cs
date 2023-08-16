using DevOps.Logger;
using System.IO;

namespace DevOps.Services.System
{
    public class MoveDirectoryService : IMoveDirectoryService
    {
        private readonly ILogger _logger;

        public MoveDirectoryService(
            ILogger logger)
        {
            _logger = logger;
        }

        public void Move(string sourceDirectory, string targetDirectory)
        {
            _logger.Log($"[MOVE] File attempt to move FROM: {sourceDirectory} | TO: {targetDirectory}");

            try
            {
                Directory.Move(sourceDirectory, targetDirectory);
            }
            catch (global::System.Exception ex)
            {
                _logger.Error($"[MOVE] FROM: {sourceDirectory} | TO: {targetDirectory} | {ex}");
            }

            if (Directory.Exists(targetDirectory))
            {
                _logger.Log($"[MOVE] File successfully move to {targetDirectory}");
            }
        }
    }
}
