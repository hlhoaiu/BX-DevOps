using Autofac;
using DevOps.Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Modules
{
    public class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestService>().As<ITestService>().SingleInstance();
            builder.RegisterType<TestService2>().As<ITestService2>().SingleInstance();
        }
    }
}
