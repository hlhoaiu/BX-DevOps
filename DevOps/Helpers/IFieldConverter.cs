using System.Collections.Generic;

namespace DevOps.Helpers
{
    public interface IFieldConverter
    {
        void MergeDict(IDictionary<string, string> baseDict, IDictionary<string, string> mergeDict);
        IDictionary<string, object?> ToDict(object config);
        IDictionary<string, string> ToStrDict(object config);
    }
}