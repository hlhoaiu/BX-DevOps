using DevOps.Logger;
using System.IO;
using System.IO.Compression;

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

        public void UnZip(string sourceZipPath, string unZipToPath)
        {
            _logger.Log($"[UNZIP] File attempt to zip | FROM: {sourceZipPath} | TO: {unZipToPath}");
            try
            {
                ZipFile.ExtractToDirectory(sourceZipPath, unZipToPath);
            }
            catch (global::System.Exception ex)
            {
                _logger.Error($"[UNZIP] FROM: {sourceZipPath} | TO: {unZipToPath} | {ex}");
            }
            if (File.Exists(unZipToPath))
            {
                _logger.Log($"[UNZIP] File successfully zip | TO: {unZipToPath}");
            }
        }
    }
}
