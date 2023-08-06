using DevOps.Helpers;
using DevOps.Logger;
using DevOps.Models.Config;
using DevOps.Serializer;
using System;

namespace DevOps.Services.Config
{
    public class UpdateJSONConfigService : IUpdateJSONConfigService
    {
        private readonly ISerializer _JSONSerializer;
        private readonly ILogger _logger;

        public UpdateJSONConfigService(
            ISerializer jSONSerializer, 
            ILogger logger)
        {
            _JSONSerializer = jSONSerializer;
            _logger = logger;
        }

        public bool Update(DeployJSONConfig updatedConfig)
        {
            var isSuccess = _JSONSerializer.TrySerialize(updatedConfig, out var jsonStr);
            if (isSuccess) 
            {
                FileHelper.WriteDeployConfigToFile(jsonStr);
                return true;
            }
            _logger.Error("Unable to Serialize updated config. No file was updated.");
            return false;
        }
    }
}
