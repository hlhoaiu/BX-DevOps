using Autofac;
using DevOps.Logger;
using DevOps.Managers;
using DevOps.Models.Config;
using DevOps.Serializer;
using DevOps.Serializer.JSON;
using DevOps.Services.Config;
using DevOps.Services.Git;
using DevOps.Services.Package;
using DevOps.Services.System;

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

            builder.RegisterType<GitMergeService>().As<IGitMergeService>().InstancePerLifetimeScope();
            builder.RegisterType<GitHashService>().As<IGitHashService>().InstancePerLifetimeScope();
            builder.RegisterType<GitZipService>().As<IGitZipService>().InstancePerLifetimeScope();
            builder.RegisterType<GitCleanService>().As<IGitCleanService>().InstancePerLifetimeScope();
            builder.RegisterType<GitManager>().As<IGitManager>().InstancePerLifetimeScope();

            builder.RegisterType<DeploymentPackageManager>().As<IDeploymentPackageManager>().InstancePerLifetimeScope();
            builder.RegisterType<GeneratePackageService>().As<IGeneratePackageService>().InstancePerLifetimeScope();
            builder.RegisterType<MovePackageService>().As<IMovePackageService>().InstancePerLifetimeScope();
            builder.RegisterType<ZipService>().As<IZipService>().InstancePerLifetimeScope();

            builder.RegisterType<MLogger>().As<ILogger>().InstancePerLifetimeScope();
        }
    }
}
