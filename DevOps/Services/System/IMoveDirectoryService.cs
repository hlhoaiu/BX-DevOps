namespace DevOps.Services.System
{
    public interface IMoveDirectoryService
    {
        void Move(string sourceDirectory, string targetDirectory);
    }
}