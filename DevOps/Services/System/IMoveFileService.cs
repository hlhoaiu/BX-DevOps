using System.Collections.Generic;

namespace DevOps.Services.System
{
    public interface IMoveFileService
    {
        void Move(string filePath, string targetDirectory);
    }
}