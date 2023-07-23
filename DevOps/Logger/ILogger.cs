using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Logger
{
    public interface ILogger
    {
        void Log(string msg);
    }
}
