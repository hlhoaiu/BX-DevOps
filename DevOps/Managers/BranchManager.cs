using DevOps.Models;
using DevOps.Models.Config;
using DevOps.Services.Git;
using System.Collections.Generic;

namespace DevOps.Managers
{
    public class BranchManager : IBranchManager
    {
        private readonly IGitMergeService _gitMergeService;
        private readonly IDeployConfigModel _deployConfigModel;

        private List<string> _mergedBranch;

        public BranchManager(
            IGitMergeService gitMergeService, 
            IDeployConfigModel deployConfigModel)
        {
            _gitMergeService = gitMergeService;
            _deployConfigModel = deployConfigModel;
        }

        public string ProposedGitDirectory { get; private set; }
        public string ProposedSourceBranch { get; private set; }
        public string ProposedTargetBranch { get; private set; }

        public void InitProposed() 
        {
            _mergedBranch = new List<string>();
            var config = _deployConfigModel.GetDeployConfig();
            ProposedGitDirectory = config.ProgramGitPath;
            ProposedSourceBranch = CommonConst.Master;
            ProposedTargetBranch = config.ReleaseBranchName;
        }

        public void Merge(string sourceBranch, string mergeBranch, string gitDirectory)
        {
            _gitMergeService.Merge(sourceBranch, mergeBranch, gitDirectory);
            _mergedBranch.Add(mergeBranch);
            if (_mergedBranch.Contains(CommonConst.Master)) 
            {
                ProposedSourceBranch = CommonConst.Production;
                ProposedTargetBranch = CommonConst.Master;
            }
        }
    }
}
