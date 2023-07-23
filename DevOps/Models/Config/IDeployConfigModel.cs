using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Models.Config
{
    public interface IDeployConfigModel
    {
        DeployConfig GetDeployConfig();

        bool UpdateDeployConfig(DeployJSONConfig updatedConfig);
    }
}
