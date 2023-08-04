namespace DevOps.Services.Git
{
    public interface IGitMergeService
    {
        void Merge(string sourceBranch, string mergeBranch, string gitDirectory);
    }
}