using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Helpers
{
    public static class FileHelper
    {
        public static string ReadFile(string path) 
        {
            return File.ReadAllText(path);
        }
    }
}
