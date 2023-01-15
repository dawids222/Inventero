using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using LibLite.Inventero.DAL.Entities;
using LibLite.Inventero.DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LibLite.Inventero.DAL.Stores
{
    public abstract class Store<TDomain, TEntity> : IStore<TDomain>
        where TDomain : Identifiable, new()
        where TEntity : Entity, new()
    {
        private readonly InventeroDbContext _context;
        private readonly IMapper _mapper;

        protected Store(InventeroDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TDomain> GetAsync(Guid id)
        {
            var entity = await _context
                .Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<TDomain>(entity);
        }

        public async Task<IEnumerable<TDomain>> GetAsync(IEnumerable<Guid> ids)
        {
            var entities = await _context
                .Set<TEntity>()
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
            return _mapper.Map<List<TDomain>>(entities);
        }

        public async Task<PaginatedList<TDomain>> GetAsync(PaginatedListRequest request)
        {
            var query = _context
                .Set<TEntity>()
                .AsQueryable();
            var count = await query.CountAsync();
            foreach (var sort in request.Sorts)
            {
                query = query.OrderBy(sort.Property, sort.Direction);
            }
            var skip = request.PageSize * request.PageIndex;
            var take = request.PageSize;
            var entities = await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            var items = _mapper.Map<List<TDomain>>(entities);
            return new(items, request.PageIndex, request.PageSize, count);
        }

        public async Task<TDomain> StoreAsync(TDomain value)
        {
            var entity = _mapper.Map<TEntity>(value);
            _context.Update(entity);
            await _context.SaveChangesAndClearAsync();
            return _mapper.Map<TDomain>(entity);
        }

        public async Task<IEnumerable<TDomain>> StoreAsync(IEnumerable<TDomain> values)
        {
            var entities = _mapper.Map<List<TEntity>>(values);
            _context
                .Set<TEntity>()
                .UpdateRange(entities);
            await _context.SaveChangesAndClearAsync();
            return _mapper.Map<List<TDomain>>(entities);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context
                .Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) { return; }
            _context.Remove(entity);
            await _context.SaveChangesAndClearAsync();
        }

        public async Task DeleteAsync(IEnumerable<Guid> ids)
        {
            var entities = await _context
                .Set<TEntity>()
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .Select(x => new TEntity { Id = x.Id })
                .ToListAsync();
            if (!entities.Any()) { return; }
            _context
                .Set<TEntity>()
                .RemoveRange(entities);
            await _context.SaveChangesAndClearAsync();
        }
    }
}
