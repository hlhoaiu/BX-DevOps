namespace DevOps.Serializer
{
    public interface ISerializer
    {
        T Deserialize<T>(string value);
        string Serialize(object obj);
        bool TryDeserialize<T>(string str, out T result);
        bool TrySerialize(object obj, out string result);
    }
}
