namespace DevOps.Managers
{
    public interface IBranchManager
    {
        string ProposedGitDirectory { get; }
        string ProposedSourceBranch { get; }
        string ProposedTargetBranch { get; }

        void InitProposed();
        void Merge(string sourceBranch, string mergeBranch, string gitDirectory);
    }
}