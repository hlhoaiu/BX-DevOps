namespace DevOps.Services.WinMerge
{
    public interface IWinMergeReportService
    {
        void GenerateReport(string path1, string path2, string reportOutputDirectory, string reportFileName);
    }
}