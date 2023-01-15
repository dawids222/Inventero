using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.DAL.Entities;
using LibLite.Inventero.DAL.Stores;
using LibLite.Inventero.DAL.Tests.Utils;

namespace LibLite.Inventero.DAL.Tests.Stores
{
    [TestFixture]
    internal class ProductStoreTests : StoreTests<ProductStore, Product, ProductEntity>
    {
        protected override Product CreateNewIdentifiable()
        {
            return new Product
            {
                Name = StringUtil.GetRandomString(),
                Price = Random.Shared.NextDouble(),
                Group = new Group
                {
                    Name = StringUtil.GetRandomString(),
                },
            };
        }

        protected override ProductEntity CreateNewEntity()
        {
            return new ProductEntity
            {
                Name = StringUtil.GetRandomString(),
                Price = Random.Shared.NextDouble(),
                Group = new GroupEntity
                {
                    Name = StringUtil.GetRandomString(),
                },
            };
        }

        protected override ProductStore CreateStore()
        {
            return new(_context, _mapperMock.Object);
        }
    }
}
