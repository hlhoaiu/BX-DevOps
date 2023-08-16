namespace DevOps.Services.Git
{
    public interface IGitZipService
    {
        void Zip(string fileFullPath, string gitHash, string gitDirectory);
    }
}