using System.Collections.Generic;

namespace DevOps.Helpers
{
    public interface IFieldHelpers
    {
        IDictionary<string, object?> ToDict(object config);
        IDictionary<string, string> ToStrDict(object config);
    }
}