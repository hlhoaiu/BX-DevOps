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
        private List<string> Logs = new List<string>();

        public Action<string> OnLogUpdated { get; set; }
        public Action<string> OnErrorUpdated { get; set; }

        public void Log(string msg)
        {
            msg = $"[LOG][{DateTime.Now}] {msg}";
            Console.WriteLine(msg);
            Logs.Add(msg);
            if (OnLogUpdated != null)
            {
                OnLogUpdated(msg);
            }
        }

        public void Error(string msg) 
        {
            msg = $"[ERROR][{DateTime.Now}] {msg}";
            Console.WriteLine(msg);
            Logs.Add(msg);
            if (OnErrorUpdated != null)
            {
                OnErrorUpdated(msg);
            }
        }
    }
}
