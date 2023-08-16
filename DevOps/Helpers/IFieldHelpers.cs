using System.Collections.Generic;

namespace DevOps.Helpers
{
    public interface IFieldHelpers
    {
        void MergeDict(IDictionary<string, string> baseDict, IDictionary<string, string> mergeDict);
        IDictionary<string, object?> ToDict(object config);
        IDictionary<string, string> ToStrDict(object config);
    }
}