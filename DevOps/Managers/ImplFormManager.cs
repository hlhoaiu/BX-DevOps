using DevOps.Helpers;
using DevOps.Models.Config;
using DevOps.Services.Form;
using DevOps.Services.System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DevOps.Managers
{
    public class ImplFormManager : IImplFormManager
    {
        private readonly IReplaceWordContentService _replaceWordContentService;
        private readonly IDeployConfigModel _deployConfigModel;
        private readonly ICopyFileService _copyFileService;
        private readonly IFieldConverter _fieldHelpers;
        private readonly IOpenDirectoryService _openDirectoryService;

        public ImplFormManager(
            IReplaceWordContentService replaceWordContentService,
            IDeployConfigModel deployConfigModel,
            ICopyFileService copyPackageService,
            IFieldConverter fieldHelpers,
            IOpenDirectoryService openDirectoryService)
        {
            _replaceWordContentService = replaceWordContentService;
            _deployConfigModel = deployConfigModel;
            _copyFileService = copyPackageService;
            _fieldHelpers = fieldHelpers;
            _openDirectoryService = openDirectoryService;
        }

        public void Generate(IDictionary<string, string> fieldsFromUser)
        {
            var config = _deployConfigModel.GetDeployConfig();
            var configDict = _fieldHelpers.ToStrDict(config).ToDictionary(kvp=>$"<{kvp.Key}>", kvp => kvp.Value);
            _fieldHelpers.MergeDict(configDict, fieldsFromUser.ToDictionary(kvp => $"<{kvp.Key}>", kvp => kvp.Value));
            var formPath = Path.Combine(config.PackageBasePath, config.DeploymentFormName);
            _replaceWordContentService.Replace(configDict, formPath);
            _openDirectoryService.Open(config.PackageBasePath);
            var backupDirectories = config.CustomPackageBackUpPaths;
            _copyFileService.Copy(formPath, backupDirectories);
            _openDirectoryService.Open(backupDirectories);
        }
    }
}
