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
            try
            {
                var jsonStr = _JSONSerializer.Serialize(updatedConfig);
                FileHelper.WriteDeployConfigToFile(jsonStr);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return false;
            }
            return true;
        }
    }
}
