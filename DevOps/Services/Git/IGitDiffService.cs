namespace DevOps.Services.Git
{
    public interface IGitDiffService
    {
        void Diff(string oldHash, string newHash, string gitDirectory);
    }
}