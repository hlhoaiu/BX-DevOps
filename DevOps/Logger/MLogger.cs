using DevOps.Helpers;
using System;
using System.IO;

namespace DevOps.Logger
{
    public class MLogger : ILogger
    {
        public string CombinedLogs { get; private set; }
        public Action<string> OnLogUpdated { get; set; }
        public Action<string> OnErrorUpdated { get; set; }

        private DateTime? _initDateTime = null;

        public void Log(string msg)
        {
            msg = $"[{DateTime.Now}][LOG] {msg}";
            Console.WriteLine(msg);
            WriteLog(msg);
            CombinedLogs += msg + "\n";
            if (OnLogUpdated != null)
            {
                OnLogUpdated(msg);
            }
        }

        public void Error(string msg) 
        {
            msg = $"[{DateTime.Now}][ERROR] {msg}";
            Console.WriteLine(msg);
            WriteLog(msg);
            CombinedLogs += msg + "\n";
            if (OnErrorUpdated != null)
            {
                OnErrorUpdated(msg);
            }
        }

        private void WriteLog(string msg) 
        {
            _initDateTime = _initDateTime ?? DateTime.Now;
            var dateTimeStr = _initDateTime.Value.ToString("yyyyMMddHHmmss");
            Directory.CreateDirectory(PathProvider.LogDirectory);
            var logPath = Path.Combine(PathProvider.LogDirectory, $"log_{dateTimeStr}.txt");
            File.AppendAllText(logPath, msg + Environment.NewLine);
        }
    }
}
