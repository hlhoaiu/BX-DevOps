namespace DevOps.Services.Git
{
    public interface IGitMergeService
    {
        void Merge(string sourceBranch, string targetBranch, string gitDirectory);
    }
}