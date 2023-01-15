using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.DAL.Entities;

namespace LibLite.Inventero.DAL.Stores
{
    public class ProductStore : Store<Product, ProductEntity>, IproductStore
    {
        public ProductStore(InventeroDbContext context, IMapper mapper)
            : base(context, mapper) { }
    }
}
