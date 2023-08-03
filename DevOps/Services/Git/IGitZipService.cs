namespace DevOps.Services.Git
{
    public interface IGitZipService
    {
        void Zip(string fileFullPath, string gitHead, string gitDirectory);
    }
}