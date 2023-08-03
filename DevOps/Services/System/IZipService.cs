namespace DevOps.Services.System
{
    public interface IZipService
    {
        void Zip(string sourceFolder, string zipPath);
    }
}