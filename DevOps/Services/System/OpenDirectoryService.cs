using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DevOps.Services.System
{
    public class OpenDirectoryService : IOpenDirectoryService
    {
        public void Open(IEnumerable<string> directories)
        {
            foreach (var directory in directories)
            {
                Open(directory);
            }
        }

        public void Open(string directory) 
        {
            if (!Directory.Exists(directory)) return;

            ProcessStartInfo startInfo = new ProcessStartInfo 
            {
                Arguments = directory,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }
    }
}
