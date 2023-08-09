using DevOps.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.System
{
    public class ZipService : IZipService
    {
        private readonly ILogger _logger;

        public ZipService(
            ILogger logger)
        {
            _logger = logger;
        }

        public void Zip(string sourceFolder, string zipPath)
        {
            _logger.Log($"[ZIP] File attempt to zip | FROM: {sourceFolder} | TO: {zipPath}");
            try
            {
                ZipFile.CreateFromDirectory(sourceFolder, zipPath);
            }
            catch (global::System.Exception ex)
            {
                _logger.Error($"[ZIP] FROM: {sourceFolder} | TO: {zipPath} | {ex}");
            }
            if (File.Exists(zipPath))
            {
                _logger.Log($"[ZIP] File successfully zip | TO: {zipPath}");
            }
        }
    }
}
