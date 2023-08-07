﻿using DevOps.Logger;
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
            var targetPath = Path.Combine(targetDirectory, fileName);
            _logger.Log($"[MOVE] File attempt to move FROM: {filePath} | TO: {targetPath}");
            File.Move(filePath, targetPath, true);
            if (File.Exists(targetPath))
            {
                _logger.Log($"[MOVE] File successfully move to {targetPath}");
            }
        }
    }
}