using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Helpers
{
    public class CommandLineHelpers
    {
        public static void Run(string command, out string output, out string error, string directory = null)
        {
            using Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    Arguments = "/c " + command,
                    CreateNoWindow = false,
                    WorkingDirectory = directory ?? string.Empty,
                }
            };
            process.Start();
            process.WaitForExit();
            output = process.StandardOutput.ReadToEnd();
            error = process.StandardError.ReadToEnd();
        }

    }
}
