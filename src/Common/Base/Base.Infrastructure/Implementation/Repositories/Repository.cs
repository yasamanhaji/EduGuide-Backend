using Base.Application.Contracts;
using Base.Domain.Entities.Common;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Base.Infrastructure.Implementation
{
    public class Repository<TEntity, TContext> : IRepository<TEntity, TContext>
        where TEntity : class, IBaseEntity
        where TContext : IDbContext
    {
        protected readonly DbSet<TEntity> _entites;
        private TContext _dbContext;

        public Repository(TContext dbContext)
        {
            _entites = dbContext.Set<TEntity>();
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entites.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entites.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _entites.AnyAsync(filter);
        }

        public bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            return _entites.Any(filter);
        }

        public async Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null
            )
        {
            var query = _entites.AsQueryable();
            if (includes != null)
                query = includes(query);
            if (filter != null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> LastOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            )
        {
            var query = _entites.AsQueryable();
            if (includes != null)
                query = includes(query);
            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);

            return await query.LastOrDefaultAsync();
        }

        public void DeleteAsync(TEntity entity)
        {
            _entites.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entites.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null
            )
        {
            var query = _entites.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes(query);
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(long? id)
        {
            return await _entites.FindAsync(id);
        }

        public async Task<List<TResult>> GetDTOAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null
            )
        {
            var query = _entites.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes(query);

            if (orderBy != null)
                query = orderBy(query);

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);

            return await query.Select(selector).ToListAsync();
        }

        public async Task<TResult> GetOneDTOAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null
            )
        {
            var query = _entites.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes(query);

            return await query.Select(selector).FirstOrDefaultAsync();
        }

        public void UpdateAsync(TEntity entity)
        {
            _entites.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = _entites.AsQueryable();
            
            if (filter != null)
                query = query.Where(filter);

            return await query.CountAsync();
        }
    }
}