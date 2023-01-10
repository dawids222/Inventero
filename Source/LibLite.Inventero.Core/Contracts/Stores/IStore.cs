namespace LibLite.Inventero.Core.Contracts.Stores
{
    public interface IStore<T>
    {
        Task<IEnumerable<T>> GetAsync(IEnumerable<Guid> ids);
        Task<T> GetAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
