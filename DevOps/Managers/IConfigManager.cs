using DevOps.Models.Config;

namespace DevOps.Managers
{
    public interface IConfigManager
    {
        DeployConfig Retrieve();
        bool Update();
    }
}
