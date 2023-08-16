using Autofac;
using DevOps.Helpers;
using DevOps.Logger;
using DevOps.Managers;
using DevOps.Models.Config;
using DevOps.Serializer;
using DevOps.Serializer.JSON;
using DevOps.Services.Config;
using DevOps.Services.Form;
using DevOps.Services.Git;
using DevOps.Services.System;
using DevOps.Services.WinMerge;

namespace DevOps.Modules
{
    class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<CommandLineRunner>().As<ICommandLineRunner>().InstancePerLifetimeScope();
            builder.RegisterType<FieldHelpers>().As<IFieldHelpers>().InstancePerLifetimeScope();

            builder.RegisterType<JSONSerializer>().As<ISerializer>().InstancePerLifetimeScope();
            builder.RegisterType<RetrieveJSONConfigService>().As<IRetrieveJSONConfigService>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateJSONConfigService>().As<IUpdateJSONConfigService>().InstancePerLifetimeScope();

            builder.RegisterType<DeployConfigModel>().As<IDeployConfigModel>().InstancePerLifetimeScope();

            //Manager
            builder.RegisterType<ConfigManager>().As<IConfigManager>().InstancePerLifetimeScope();
            builder.RegisterType<BranchManager>().As<IBranchManager>().InstancePerLifetimeScope();
            builder.RegisterType<DeploymentPackageManager>().As<IDeploymentPackageManager>().InstancePerLifetimeScope();
            builder.RegisterType<ImplFormManager>().As<IImplFormManager>().InstancePerLifetimeScope();
            builder.RegisterType<BackupManager>().As<IBackupManager>().InstancePerLifetimeScope();
            builder.RegisterType<DeployManager>().As<IDeployManager>().InstancePerLifetimeScope();
            
            //Git
            builder.RegisterType<GitMergeService>().As<IGitMergeService>().InstancePerLifetimeScope();
            builder.RegisterType<GitHashService>().As<IGitHashService>().InstancePerLifetimeScope();
            builder.RegisterType<GitZipService>().As<IGitZipService>().InstancePerLifetimeScope();
            builder.RegisterType<GitCleanService>().As<IGitCleanService>().InstancePerLifetimeScope();
            builder.RegisterType<GitDiffService>().As<IGitDiffService>().InstancePerLifetimeScope();

            //System
            builder.RegisterType<MoveDirectoryService>().As<IMoveDirectoryService>().InstancePerLifetimeScope();
            builder.RegisterType<MoveFileService>().As<IMoveFileService>().InstancePerLifetimeScope();
            builder.RegisterType<CopyFileService>().As<ICopyFileService>().InstancePerLifetimeScope();
            builder.RegisterType<ZipService>().As<IZipService>().InstancePerLifetimeScope();

            //WinMerge
            builder.RegisterType<WinMergeReportService>().As<IWinMergeReportService>().InstancePerLifetimeScope();
            builder.RegisterType<WinMergeCompareService>().As<IWinMergeCompareService>().InstancePerLifetimeScope();

            //Form
            builder.RegisterType<ReplaceWordContentService>().As<IReplaceWordContentService>().InstancePerLifetimeScope();

            //Package
            builder.RegisterType<GeneratePackageService>().As<IGeneratePackageService>().InstancePerLifetimeScope();
        }
    }
}
