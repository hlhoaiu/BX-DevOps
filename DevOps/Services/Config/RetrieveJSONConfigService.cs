using DevOps.Helpers;
using DevOps.Models.Config;
using DevOps.Serializer;
using System.IO;

namespace DevOps.Services.Config
{
    public class RetrieveJSONConfigService : IRetrieveJSONConfigService
    {
        private readonly ISerializer _JSONSerializer;

        public RetrieveJSONConfigService(ISerializer jSONSerializer)
        {
            _JSONSerializer = jSONSerializer;
        }

        public DeployJSONConfig Retrieve()
        {
            var jsonStr = FileHelper.GetDeployConfigFileText();
            var jsonConfig = _JSONSerializer.Deserialize<DeployJSONConfig>(jsonStr);
            return jsonConfig;
        }
    }
}
