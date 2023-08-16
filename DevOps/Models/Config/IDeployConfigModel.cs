namespace DevOps.Models.Config
{
    public interface IDeployConfigModel
    {
        DeployJSONConfig GetDeployJSONConfig();
        DeployConfig GetDeployConfig();

        bool UpdateDeployConfig(DeployJSONConfig updatedConfig);
    }
}
