using DevOps.Models.Config;
using System.Collections.Generic;

namespace DevOps.Services.Form
{
    public interface IReplaceWordContentService
    {
        void Replace(IDictionary<string, string> replaceDict, string saveAsPath);
    }
}