using DevOps.Models.Config;

namespace DevOps.Services.Config
{
    public interface IRetrieveJSONConfigService
    {
        DeployJSONConfig Retrieve();
    }
}
