using LibLite.Inventero.Core.Models.Domain;

namespace LibLite.Inventero.Core.Contracts.Services
{
    public interface IInventoryService
    {
        Task<Inventory> CreateAsync();
        Task<Inventory> CreateAsync(DateTime from);
        Task<Inventory> CreateAsync(DateTime from, DateTime to);
        Task<Inventory> UpdateAsync(Inventory inventory);
    }
}
