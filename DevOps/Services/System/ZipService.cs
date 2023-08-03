using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.System
{
    public class ZipService : IZipService
    {
        public void Zip(string sourceFolder, string zipPath)
        {
            ZipFile.CreateFromDirectory(sourceFolder, zipPath);
        }
    }
}
