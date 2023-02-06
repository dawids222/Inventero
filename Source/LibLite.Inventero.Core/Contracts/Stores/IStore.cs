using LibLite.Inventero.Core.Models.Pagination;

namespace LibLite.Inventero.Core.Contracts.Stores
{
    public interface IStore<T>
    {
        Task<T> GetAsync(long id);
        Task<IEnumerable<T>> GetAsync(IEnumerable<long> ids);
        Task<PaginatedList<T>> GetAsync(PaginatedListRequest request);
        Task<T> StoreAsync(T value);
        Task<IEnumerable<T>> StoreAsync(IEnumerable<T> values);
        Task DeleteAsync(long id);
        Task DeleteAsync(IEnumerable<long> ids);
    }
}
