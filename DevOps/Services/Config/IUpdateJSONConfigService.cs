using DevOps.Models.Config;

namespace DevOps.Services.Config
{
    public interface IUpdateJSONConfigService
    {
        bool Update(DeployJSONConfig updatedConfig);
    }
}
