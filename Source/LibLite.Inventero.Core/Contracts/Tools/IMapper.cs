namespace LibLite.Inventero.Core.Contracts.Tools
{
    public interface IMapper
    {
        T Map<T>(object source);
    }
}
