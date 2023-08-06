namespace DevOps.Helpers
{
    public interface ICommandLineRunner
    {
        void Run(string command, out string output, out string error, string directory = null);
    }
}