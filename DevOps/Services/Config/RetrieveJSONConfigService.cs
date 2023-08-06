using DevOps.Helpers;
using DevOps.Logger;
using DevOps.Models.Config;
using DevOps.Serializer;
using System.IO;

namespace DevOps.Services.Config
{
    public class RetrieveJSONConfigService : IRetrieveJSONConfigService
    {
        private readonly ISerializer _JSONSerializer;
        private readonly ILogger _logger;

        public RetrieveJSONConfigService(
            ISerializer jSONSerializer, 
            ILogger logger)
        {
            _JSONSerializer = jSONSerializer;
            _logger = logger;
        }

        public DeployJSONConfig Retrieve()
        {
            var jsonStr = FileHelper.GetDeployConfigFileText();
            var isSuccess = _JSONSerializer.TryDeserialize<DeployJSONConfig>(jsonStr, out var jsonConfig);
            if (!isSuccess) 
            {
                _logger.Error($"Unable to Deserialize config. Retrieved an empty {nameof(DeployJSONConfig)}.");
                return new DeployJSONConfig();
            }
            return jsonConfig;
        }
    }
}
