using DevOps.Models.Config;

namespace DevOps.Managers
{
    public interface IConfigManager
    {
        DeployConfig GetDeployConfig();
        DeployJSONConfig GetDeployJSONConfig();
        bool Update(DeployJSONConfig deployJSONConfig);
    }
}
