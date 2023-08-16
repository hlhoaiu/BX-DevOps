namespace DevOps.Services.System
{
    public interface IZipService
    {
        void UnZip(string sourceZipPath, string unZipToPath);
        void Zip(string sourceFolder, string zipPath);
    }
}