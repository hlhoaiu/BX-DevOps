using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.Test
{
    public class TestService : ITestService
    {
        private readonly ITestService2 testService2;

        public TestService(ITestService2 testService2)
        {
            this.testService2 = testService2;
        }

        public string TestString()
        {
            return testService2.TestString();
        }
    }
}
