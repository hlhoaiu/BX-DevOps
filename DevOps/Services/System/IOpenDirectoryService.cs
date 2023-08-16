using System.Collections.Generic;

namespace DevOps.Services.System
{
    public interface IOpenDirectoryService
    {
        void Open(string directory);
        void Open(IEnumerable<string> directories);
    }
}