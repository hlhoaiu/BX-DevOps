namespace DevOps.Services.System
{
    public interface ICheckPathService
    {
        bool IsValid(string directory);
    }
}