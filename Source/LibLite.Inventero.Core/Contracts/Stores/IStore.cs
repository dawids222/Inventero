using LibLite.Inventero.Core.Models.Pagination;

namespace LibLite.Inventero.Core.Contracts.Stores
{
    public interface IStore<T>
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAsync(IEnumerable<Guid> ids);
        Task<PaginatedList<T>> GetAsync(PaginatedListRequest request);
        Task<T> StoreAsync(T value);
        Task<IEnumerable<T>> StoreAsync(IEnumerable<T> values);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(IEnumerable<Guid> ids);
    }
}
