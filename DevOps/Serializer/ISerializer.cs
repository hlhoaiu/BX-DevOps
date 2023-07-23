using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Serializer
{
    public interface ISerializer
    {
        T Deserialize<T>(string value);
        string Serializer(object obj);
        bool TryDeserialize<T>(string str, out T result);
        bool TrySerialize(object obj, out string result);
    }
}
