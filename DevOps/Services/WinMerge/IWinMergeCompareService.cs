namespace DevOps.Services.WinMerge
{
    public interface IWinMergeCompareService
    {
        void Compare(string path1, string path2);
    }
}