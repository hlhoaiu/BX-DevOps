using DevOps.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Services.Config
{
    public interface IUpdateJSONConfigService
    {
        bool Update(DeployJSONConfig updatedConfig);
    }
}
