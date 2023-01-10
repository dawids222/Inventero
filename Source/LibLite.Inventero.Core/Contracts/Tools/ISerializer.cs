namespace LibLite.Inventero.Core.Contracts.Tools
{
    public interface ISerializer
    {
        string Serialize(object value);
        T Deserialize<T>(string value);
    }
}
