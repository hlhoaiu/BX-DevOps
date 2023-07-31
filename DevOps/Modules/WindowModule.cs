using Autofac;
using DevOps.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Modules
{
    public class WindowModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().SingleInstance();
            builder.RegisterType<ConfigPage>().As<IConfigPage>().SingleInstance();
            builder.RegisterType<BranchPage>().As<IBranchPage>().SingleInstance();
            builder.RegisterType<PackagePage>().As<IPackagePage>().SingleInstance();
            builder.RegisterType<FormPage>().As<IFormPage>().SingleInstance();
            builder.RegisterType<BackupPage>().As<IBackupPage>().SingleInstance();
            builder.RegisterType<DeploymentPage>().As<IDeploymentPage>().SingleInstance();
        }
    }
}
