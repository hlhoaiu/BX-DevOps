namespace DevOps.Services.Git
{
    public interface IGitCleanService
    {
        void Clean(string gitDirectory);
    }
}