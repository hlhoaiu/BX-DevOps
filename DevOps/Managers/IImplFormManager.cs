using System.Collections.Generic;

namespace DevOps.Managers
{
    public interface IImplFormManager
    {
        void Generate(IDictionary<string, string> fieldsFromUser);
    }
}