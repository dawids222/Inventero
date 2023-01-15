using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.DAL.Entities;

namespace LibLite.Inventero.DAL.Stores
{
    public class PurchaseStore : Store<Purchase, PurchaseEntity>, IPurchaseStore
    {
        public PurchaseStore(InventeroDbContext context, IMapper mapper)
            : base(context, mapper) { }
    }
}
