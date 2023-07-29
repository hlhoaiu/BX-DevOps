using Autofac;
using DevOps.Logger;
using DevOps.Managers;
using DevOps.Models.Config;
using DevOps.Serializer;
using DevOps.Serializer.JSON;
using DevOps.Services.Config;

namespace DevOps.Modules
{
    class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigManager>().As<IConfigManager>().InstancePerLifetimeScope();

            builder.RegisterType<DeployConfigModel>().As<IDeployConfigModel>().InstancePerLifetimeScope();

            builder.RegisterType<JSONSerializer>().As<ISerializer>().InstancePerLifetimeScope();
            builder.RegisterType<RetrieveJSONConfigService>().As<IRetrieveJSONConfigService>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateJSONConfigService>().As<IUpdateJSONConfigService>().InstancePerLifetimeScope();

            builder.RegisterType<MLogger>().As<ILogger>().InstancePerLifetimeScope();
        }
    }
}
