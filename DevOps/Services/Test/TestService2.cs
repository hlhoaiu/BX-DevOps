using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.Test
{
    public class TestService2 : ITestService2
    {
        public string TestString()
        {
            return "myTest";
        }
    }
}
