using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.DAL.Entities;
using LibLite.Inventero.DAL.Extensions;
using LibLite.Inventero.DAL.Stores;
using LibLite.Inventero.DAL.Tests.Utils;
using Microsoft.EntityFrameworkCore;

namespace LibLite.Inventero.DAL.Tests.Stores
{
    [TestFixture]
    internal class PurchasesStoreTests : StoreTests<PurchaseStore, Purchase, PurchaseEntity>
    {
        protected override Purchase CreateNewIdentifiable()
        {
            return new Purchase
            {
                Amount = Random.Shared.Next(0, 100),
                UnitPrice = Random.Shared.NextDouble(),
                Date = DateTime.UtcNow,
                Product = new Product
                {
                    Name = StringUtil.GetRandomString(),
                    Price = Random.Shared.NextDouble(),
                    Group = new Group
                    {
                        Name = StringUtil.GetRandomString(),
                    },
                },
            };
        }

        protected override PurchaseEntity CreateNewEntity()
        {
            return new PurchaseEntity
            {
                Amount = Random.Shared.Next(0, 100),
                UnitPrice = Random.Shared.NextDouble(),
                Date = DateTime.UtcNow,
                Product = new ProductEntity
                {
                    Name = StringUtil.GetRandomString(),
                    Price = Random.Shared.NextDouble(),
                    Group = new GroupEntity
                    {
                        Name = StringUtil.GetRandomString(),
                    },
                },
            };
        }

        protected override PurchaseStore CreateStore()
        {
            return new(_context, _mapperMock.Object);
        }

        [Test]
        public async Task SaveChangesAsync_UtcDateTime_StoresDateTimeAsUtc()
        {
            var purchase = CreateNewEntity();

            _context.Add(purchase);
            await _context.SaveChangesAndClearAsync();

            var entity = await _context
                .Purchases
                .FirstOrDefaultAsync(x => x.Id == purchase.Id);
            Assert.That(entity.Date, Is.EqualTo(purchase.Date));
            Assert.That(entity.Date.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        [Test]
        public async Task SaveChangesAsync_LocalDateTime_StoresDateTimeAsUtc()
        {
            var purchase = CreateNewEntity();
            purchase.Date = DateTime.SpecifyKind(purchase.Date, DateTimeKind.Local);

            _context.Add(purchase);
            await _context.SaveChangesAndClearAsync();

            var entity = await _context
                .Purchases
                .FirstOrDefaultAsync(x => x.Id == purchase.Id);
            Assert.That(entity.Date, Is.EqualTo(purchase.Date.ToUniversalTime()));
            Assert.That(entity.Date.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        [Test]
        public async Task SaveChangesAsync_UnspecifiedDateTime_StoresDateTimeAsUtc()
        {
            var purchase = CreateNewEntity();
            purchase.Date = DateTime.SpecifyKind(purchase.Date, DateTimeKind.Unspecified);

            _context.Add(purchase);
            await _context.SaveChangesAndClearAsync();

            var entity = await _context
                .Purchases
                .FirstOrDefaultAsync(x => x.Id == purchase.Id);
            Assert.That(entity.Date, Is.EqualTo(DateTime.SpecifyKind(purchase.Date, DateTimeKind.Utc)));
            Assert.That(entity.Date.Kind, Is.EqualTo(DateTimeKind.Utc));
        }
    }
}
