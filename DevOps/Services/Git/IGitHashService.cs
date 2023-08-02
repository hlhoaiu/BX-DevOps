namespace DevOps.Services.Git
{
    public interface IGitHashService
    {
        string GetHash(string branch, string gitDirectory);
    }
}