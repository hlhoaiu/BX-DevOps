using DevOps.Services.WinMerge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Managers
{
    public class DeployManager : IDeployManager
    {
        private readonly IWinMergeCompareService _winMergeCompareService;

        public DeployManager(IWinMergeCompareService winMergeCompareService)
        {
            _winMergeCompareService = winMergeCompareService;
        }

        public void Deploy()
        {
            _winMergeCompareService.Compare();
        }
    }
}
