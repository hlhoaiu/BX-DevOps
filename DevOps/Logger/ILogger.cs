using System;

namespace DevOps.Logger
{
    public interface ILogger
    {
        Action<string> OnLogUpdated { get; set; }
        Action<string> OnErrorUpdated { get; set; }
        string CombinedLogs { get; }

        void Log(string msg);
        void Error(string msg);
    }
}
