using DevOps.Helpers;
using DevOps.Models.Config;
using DevOps.Services.Form;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Managers
{
    public class ImplFormManager : IImplFormManager
    {
        private readonly IReplaceWordContentService _replaceWordContentService;
        private readonly IDeployConfigModel _deployConfigModel;

        public ImplFormManager(
            IReplaceWordContentService replaceWordContentService, 
            IDeployConfigModel deployConfigModel)
        {
            _replaceWordContentService = replaceWordContentService;
            _deployConfigModel = deployConfigModel;
        }

        public void Release()
        {
            var config = _deployConfigModel.GetDeployConfig();
            var configDict = FieldHelpers.ToStrDict(config).ToDictionary(kvp=>$"<{kvp.Key}>", kvp => kvp.Value);
            var formReleasePath = Path.Combine(config.PackageReleasePath, config.DeploymentFormName);
            _replaceWordContentService.Replace(configDict, formReleasePath);
        }
    }
}
