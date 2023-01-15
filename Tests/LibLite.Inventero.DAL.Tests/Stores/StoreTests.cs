using KellermanSoftware.CompareNetObjects;
using LibLite.Inventero.Core.Consts;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using LibLite.Inventero.DAL.Entities;
using LibLite.Inventero.DAL.Extensions;
using LibLite.Inventero.DAL.Stores;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LibLite.Inventero.DAL.Tests.Stores
{
    [TestFixture]
    internal abstract class StoreTests<TStore, TDomain, TEntity>
        where TStore : Store<TDomain, TEntity>
        where TDomain : Identifiable, new()
        where TEntity : Entity, new()
    {
        protected InventeroDbContext _context;
        protected Mock<IMapper> _mapperMock;

        private CompareLogic _comparer;

        protected TStore _store;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<InventeroDbContext>()
                .UseInMemoryDatabase($"StoreTests_{DateTime.UtcNow:yyyy.MM.dd HH:mm:ss:ffff}");
            _context = new(options.Options);
            _mapperMock = new();
            _comparer = new();

            _store = CreateStore();

            await _context.Database.EnsureCreatedAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await _context.Database.EnsureDeletedAsync();
        }

        protected abstract TStore CreateStore();
        protected abstract TEntity CreateNewEntity();
        protected abstract TDomain CreateNewIdentifiable();

        [Test]
        public async Task GetAsync_ById_ReturnsObjectWithGivenId()
        {
            var expected = new TDomain();
            var entity = CreateNewEntity();
            _context.Add(CreateNewEntity());
            _context.Add(entity);
            _context.Add(CreateNewEntity());
            await _context.SaveChangesAsync();
            _mapperMock
                .Setup(x => x.Map<TDomain>(It.Is<TEntity>(x => _comparer.Compare(x, entity).AreEqual)))
                .Returns(expected);

            var result = await _store.GetAsync(entity.Id);

            Assert.That(result, Is.EqualTo(expected));
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task GetAsync_ById_MapperThrows_ThrowsTheSameException()
        {
            var exception = new Exception("Error!");
            var entity = CreateNewEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            _mapperMock
                .Setup(x => x.Map<TDomain>(It.IsAny<TEntity>()))
                .Throws(exception);

            Task act() => _store.GetAsync(entity.Id);

            Assert.ThrowsAsync<Exception>(act, exception.Message);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task GetAsync_ByIds_ReturnsCollectionOfObjectsWithGivenIds()
        {
            var expected = new List<TDomain> { new(), new() };
            var entity1 = CreateNewEntity();
            var entity2 = CreateNewEntity();
            _context.Add(CreateNewEntity());
            _context.Add(entity1);
            _context.Add(entity2);
            _context.Add(CreateNewEntity());
            await _context.SaveChangesAsync();
            _mapperMock
                .Setup(x => x.Map<List<TDomain>>(
                    It.Is<List<TEntity>>(x =>
                        x.All(x =>
                            _comparer.Compare(x, entity1).AreEqual ||
                            _comparer.Compare(x, entity2).AreEqual))))
                .Returns(expected);

            var ids = new Guid[] { entity1.Id, entity2.Id };
            var result = await _store.GetAsync(ids);

            Assert.That(result, Is.EqualTo(expected));
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task GetAsync_ByIds_MapperThrows_ThrowsTheSameException()
        {
            var exception = new Exception("Error!");
            var entity = CreateNewEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            _mapperMock
                .Setup(x => x.Map<List<TDomain>>(It.IsAny<List<TEntity>>()))
                .Throws(exception);

            var ids = new Guid[] { entity.Id };
            Task act() => _store.GetAsync(ids);

            Assert.ThrowsAsync<Exception>(act, exception.Message);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task GetAsync_Paginated_WithoutSort_ReturnsPaginatedCollectionOfObjects()
        {
            var pageIndex = 1;
            var pageSize = 2;
            var mapped = new List<TDomain> { new(), new() };
            var expected = new PaginatedList<TDomain>(
                items: mapped,
                pageIndex: pageIndex,
                pageSize: pageSize,
                totalItems: 4);
            var entity1 = CreateNewEntity();
            var entity2 = CreateNewEntity();
            _context.Add(CreateNewEntity());
            _context.Add(CreateNewEntity());
            _context.Add(entity1);
            _context.Add(entity2);
            await _context.SaveChangesAsync();
            _mapperMock
                .Setup(x => x.Map<List<TDomain>>(
                    It.Is<List<TEntity>>(x =>
                        x.All(x =>
                            _comparer.Compare(x, entity1).AreEqual ||
                            _comparer.Compare(x, entity2).AreEqual))))
                .Returns(mapped);

            var request = new PaginatedListRequest(pageIndex, pageSize);
            var result = await _store.GetAsync(request);

            Assert.That(_comparer.Compare(expected, result).AreEqual, Is.True);
            Assert.That(result.TotalPages, Is.EqualTo(2));
            Assert.That(result.HasNextPage, Is.False);
            Assert.That(result.HasPreviousPage, Is.True);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task GetAsync_Paginated_WithSort_ReturnsSortedPaginatedCollectionOfObjects()
        {
            var pageIndex = 0;
            var pageSize = 2;
            var mapped = new List<TDomain> { new(), new() };
            var expected = new PaginatedList<TDomain>(
                items: mapped,
                pageIndex: pageIndex,
                pageSize: pageSize,
                totalItems: 4);
            var entity1 = CreateNewEntity();
            entity1.Id = Guid.Parse("00000000-0000-0000-0000-000000000002");
            var entity2 = CreateNewEntity();
            entity2.Id = Guid.Parse("00000000-0000-0000-0000-000000000001");
            _context.Add(CreateNewEntity());
            _context.Add(CreateNewEntity());
            _context.Add(entity1);
            _context.Add(entity2);
            await _context.SaveChangesAsync();
            _mapperMock
                .Setup(x => x.Map<List<TDomain>>(
                    It.Is<List<TEntity>>(x =>
                        x.ElementAt(0).Id == entity2.Id &&
                        x.ElementAt(1).Id == entity1.Id)))
                .Returns(mapped);

            var sorts = new Sort[] { new Sort
            {
                Property = nameof(Entity.Id),
                Direction = SortDirection.ASC,
            } };
            var request = new PaginatedListRequest(pageIndex, pageSize, sorts);
            var result = await _store.GetAsync(request);

            Assert.That(_comparer.Compare(expected, result).AreEqual, Is.True);
            Assert.That(result.TotalPages, Is.EqualTo(2));
            Assert.That(result.HasNextPage, Is.True);
            Assert.That(result.HasPreviousPage, Is.False);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task GetAsync_Paginated_MapperThrows_ThrowsTheSameException()
        {
            var exception = new Exception("Error!");
            var entity = CreateNewEntity();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            _mapperMock
                .Setup(x => x.Map<List<TDomain>>(It.IsAny<List<TEntity>>()))
                .Throws(exception);

            var request = new PaginatedListRequest(0, 2);
            Task act() => _store.GetAsync(request);

            Assert.ThrowsAsync<Exception>(act, exception.Message);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task StoreAsync_NewEntity_AddsEntityToDatabase()
        {
            var expected = CreateNewIdentifiable();
            var value = CreateNewIdentifiable();
            var entity = CreateNewEntity();
            _mapperMock
                .Setup(x => x.Map<TEntity>(value))
                .Returns(entity);
            _mapperMock
                .Setup(x => x.Map<TDomain>(entity))
                .Returns(expected);

            var result = await _store.StoreAsync(value);

            var isAdded = await _context
                .Set<TEntity>()
                .AnyAsync(x => x.Id == entity.Id);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(isAdded, Is.True);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task StoreAsync_ExistingEntity_UpdatesEntityInDatabase()
        {
            var expected = CreateNewIdentifiable();
            var value = CreateNewIdentifiable();
            var entity = CreateNewEntity();
            var modifiedEntity = CreateNewEntity();
            _context.Add(entity);
            await _context.SaveChangesAndClearAsync();
            modifiedEntity.Id = entity.Id;
            _mapperMock
                .Setup(x => x.Map<TEntity>(value))
                .Returns(modifiedEntity);
            _mapperMock
                .Setup(x => x.Map<TDomain>(modifiedEntity))
                .Returns(expected);

            var result = await _store.StoreAsync(value);

            var currentEntity = await _context
                .Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id);
            Assert.That(_comparer.Compare(currentEntity, entity).AreEqual, Is.False);
            Assert.That(_comparer.Compare(currentEntity, modifiedEntity).AreEqual, Is.True);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task StoreAsync_NewEntities_AddsEntitiesToDatabase()
        {
            var expected1 = CreateNewIdentifiable();
            var expected2 = CreateNewIdentifiable();
            var expected = new List<TDomain> { expected1, expected2 };
            var value1 = CreateNewIdentifiable();
            var value2 = CreateNewIdentifiable();
            var values = new List<TDomain> { value1, value2 };
            var entity1 = CreateNewEntity();
            var entity2 = CreateNewEntity();
            var entities = new List<TEntity> { entity1, entity2 };
            _mapperMock
                .Setup(x => x.Map<List<TEntity>>(values))
                .Returns(entities);
            _mapperMock
                .Setup(x => x.Map<List<TDomain>>(entities))
                .Returns(expected);

            var result = await _store.StoreAsync(values);

            var areAdded = await _context
                .Set<TEntity>()
                .AllAsync(x =>
                    x.Id == entity1.Id ||
                    x.Id == entity2.Id);
            Assert.That(_comparer.Compare(expected, result).AreEqual, Is.True);
            Assert.That(areAdded, Is.True);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task StoreAsync_ExistingEntities_UpdatesEntitiesInDatabase()
        {
            var expected1 = CreateNewIdentifiable();
            var expected2 = CreateNewIdentifiable();
            var expected = new List<TDomain> { expected1, expected2 };
            var value1 = CreateNewIdentifiable();
            var value2 = CreateNewIdentifiable();
            var values = new List<TDomain> { value1, value2 };
            var entity1 = CreateNewEntity();
            var entity2 = CreateNewEntity();
            var entities = new List<TEntity> { entity1, entity2 };
            var modifiedEntity1 = CreateNewEntity();
            var modifiedEntity2 = CreateNewEntity();
            var modifiedEntities = new List<TEntity> { modifiedEntity1, modifiedEntity2 };
            _context.AddRange(entities);
            await _context.SaveChangesAndClearAsync();
            modifiedEntity1.Id = entity1.Id;
            modifiedEntity2.Id = entity2.Id;
            _mapperMock
                .Setup(x => x.Map<List<TEntity>>(values))
                .Returns(modifiedEntities);
            _mapperMock
                .Setup(x => x.Map<List<TDomain>>(modifiedEntities))
                .Returns(expected);

            var result = await _store.StoreAsync(values);

            var currentEntities = await _context
                .Set<TEntity>()
                .AsNoTracking()
                .Where(x =>
                    x.Id == entity1.Id ||
                    x.Id == entity2.Id)
                .ToListAsync();
            Assert.That(_comparer.Compare(currentEntities, entities).AreEqual, Is.False);
            Assert.That(_comparer.Compare(currentEntities, modifiedEntities).AreEqual, Is.True);
            Assert.That(_comparer.Compare(expected, result).AreEqual, Is.True);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task StoreAsync_NewAndExistingEntities_AddsAndUpdatesEntitiesInDatabase()
        {
            var expected1 = CreateNewIdentifiable();
            var expected2 = CreateNewIdentifiable();
            var expected = new List<TDomain> { expected1, expected2 };
            var value1 = CreateNewIdentifiable();
            var value2 = CreateNewIdentifiable();
            var values = new List<TDomain> { value1, value2 };
            var entity1 = CreateNewEntity();
            var entity2 = CreateNewEntity();
            var modifiedEntity1 = CreateNewEntity();
            var entities = new List<TEntity> { modifiedEntity1, entity2 };
            _context.Add(entity1);
            await _context.SaveChangesAndClearAsync();
            modifiedEntity1.Id = entity1.Id;
            _mapperMock
                .Setup(x => x.Map<List<TEntity>>(values))
                .Returns(entities);
            _mapperMock
                .Setup(x => x.Map<List<TDomain>>(entities))
                .Returns(expected);

            var result = await _store.StoreAsync(values);

            var currentEntity1 = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == entity1.Id);
            var currentEntity2 = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == entity2.Id);
            Assert.That(_comparer.Compare(currentEntity1, modifiedEntity1).AreEqual, Is.True);
            Assert.That(_comparer.Compare(currentEntity2, entity2).AreEqual, Is.True);
            Assert.That(_comparer.Compare(expected, result).AreEqual, Is.True);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task DeleteAsync_ById_RemovesEntityFromDatabase()
        {
            var entity = CreateNewEntity();
            _context.Add(entity);
            _context.Add(CreateNewEntity());
            await _context.SaveChangesAndClearAsync();

            await _store.DeleteAsync(entity.Id);

            var exists = await _context
                .Set<TEntity>()
                .AnyAsync(x => x.Id == entity.Id);
            Assert.That(exists, Is.False);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task DeleteAsync_ById_IdDoesNotExist_RemovesNothing()
        {
            _context.Add(CreateNewEntity());
            await _context.SaveChangesAndClearAsync();

            var id = Guid.NewGuid();
            await _store.DeleteAsync(id);

            var count = await _context
                .Set<TEntity>()
                .CountAsync();
            Assert.That(count, Is.EqualTo(1));
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task DeleteAsync_ByIds_RemovesEntitiesFromDatabase()
        {
            var entity1 = CreateNewEntity();
            var entity2 = CreateNewEntity();
            _context.Add(CreateNewEntity());
            _context.Add(entity1);
            _context.Add(entity2);
            _context.Add(CreateNewEntity());
            await _context.SaveChangesAndClearAsync();

            var ids = new[] { entity1.Id, entity2.Id };
            await _store.DeleteAsync(ids);

            var exist = await _context
                .Set<TEntity>()
                .AnyAsync(x => ids.Contains(x.Id));
            Assert.That(exist, Is.False);
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task DeleteAsync_ByIds_SomeIdsDoNotExist_RemovesSomeEntitiesFromDatabase()
        {
            var entity1 = CreateNewEntity();
            var entity2 = CreateNewEntity();
            _context.Add(CreateNewEntity());
            _context.Add(entity1);
            _context.Add(entity2);
            _context.Add(CreateNewEntity());
            await _context.SaveChangesAndClearAsync();

            var ids = new[] { entity1.Id, Guid.NewGuid() };
            await _store.DeleteAsync(ids);

            var exists = await _context
                .Set<TEntity>()
                .AnyAsync(x => ids.Contains(x.Id));
            var count = await _context
                .Set<TEntity>()
                .CountAsync();
            Assert.That(exists, Is.False);
            Assert.That(count, Is.EqualTo(3));
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }

        [Test]
        public async Task DeleteAsync_ByIds_AllIdsDoNotExist_RemoveNothingFromDatabase()
        {
            var entity1 = CreateNewEntity();
            var entity2 = CreateNewEntity();
            _context.Add(CreateNewEntity());
            _context.Add(entity1);
            _context.Add(entity2);
            _context.Add(CreateNewEntity());
            await _context.SaveChangesAndClearAsync();

            var ids = new[] { Guid.NewGuid(), Guid.NewGuid() };
            await _store.DeleteAsync(ids);

            var exists = await _context
                .Set<TEntity>()
                .AnyAsync(x => ids.Contains(x.Id));
            var count = await _context
                .Set<TEntity>()
                .CountAsync();
            Assert.That(exists, Is.False);
            Assert.That(count, Is.EqualTo(4));
            Assert.That(_context.ChangeTracker.HasChanges(), Is.False);
        }
    }
}
