using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.DAL.Entities;
using LibLite.Inventero.DAL.Stores;
using LibLite.Inventero.DAL.Tests.Utils;

namespace LibLite.Inventero.DAL.Tests.Stores
{
    [TestFixture]
    internal class GroupStoreTests : StoreTests<GroupStore, Group, GroupEntity>
    {
        protected override Group CreateNewIdentifiable()
        {
            return new Group
            {
                Name = StringUtil.GetRandomString(),
            };
        }

        protected override GroupEntity CreateNewEntity()
        {
            return new GroupEntity
            {
                Name = StringUtil.GetRandomString(),
            };
        }

        protected override GroupStore CreateStore()
        {
            return new(_context, _mapperMock.Object);
        }
    }
}
