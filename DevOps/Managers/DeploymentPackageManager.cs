using DevOps.Services.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Managers
{
    public class DeploymentPackageManager : IDeploymentPackageManager
    {
        private readonly IGeneratePackageService _generatePackageService;
        private readonly IMovePackageService _movePackageService;

        public DeploymentPackageManager(
            IGeneratePackageService generatePackageService, 
            IMovePackageService movePackageService)
        {
            _generatePackageService = generatePackageService;
            _movePackageService = movePackageService;
        }

        public void Release()
        {
            _generatePackageService.Generate();
            //_movePackageService.Move();
        }
    }
}
