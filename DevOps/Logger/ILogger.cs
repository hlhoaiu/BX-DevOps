using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Logger
{
    public interface ILogger
    {
        Action<string> OnLogUpdated { get; set; }
        Action<string> OnErrorUpdated { get; set; }
        void Log(string msg);
        void Error(string msg);
    }
}
