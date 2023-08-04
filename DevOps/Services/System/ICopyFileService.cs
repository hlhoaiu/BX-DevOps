using System.Collections.Generic;

namespace DevOps.Services.System
{
    public interface ICopyFileService
    {
        void Copy(string filePath, IEnumerable<string> targetDirectories);
    }
}