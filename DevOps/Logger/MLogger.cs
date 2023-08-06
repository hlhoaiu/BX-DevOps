using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace DevOps.Logger
{
    public class MLogger : ILogger
    {
        private List<string> _logs = new List<string>();

        public string CombinedLogs { get; private set; }

        public Action<string> OnLogUpdated { get; set; }
        public Action<string> OnErrorUpdated { get; set; }

        public void Log(string msg)
        {
            msg = $"[LOG][{DateTime.Now}] {msg}";
            Console.WriteLine(msg);
            _logs.Add(msg);
            CombinedLogs += msg + "\n";
            if (OnLogUpdated != null)
            {
                OnLogUpdated(msg);
            }
        }

        public void Error(string msg) 
        {
            msg = $"[ERROR][{DateTime.Now}] {msg}";
            Console.WriteLine(msg);
            _logs.Add(msg);
            CombinedLogs += msg + "\n";
            if (OnErrorUpdated != null)
            {
                OnErrorUpdated(msg);
            }
        }
    }
}
