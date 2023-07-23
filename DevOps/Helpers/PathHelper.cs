using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Helpers
{
    public static class PathHelper
    {
        public static string GetProjectDirectory() 
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        }

        public static string GetConfigPath() 
        {
            return Path.Combine(GetProjectDirectory(), "Configs", "Configuration.json");
        }
    }
}
