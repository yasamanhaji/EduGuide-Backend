using Base.Application.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Base.Infrastructure.Implementation
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> 
        where TContext : IDbContext
    {
        protected TContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed = false;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            _repositories = new Dictionary<Type, object>();
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                //await _transaction.RollbackAsync();
                throw new Exception(ex.ToString());
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        //public IRepository<T> GetRepository<T>() where T : class
        //{
        //    if (_repositories.ContainsKey(typeof(T)))
        //        return _repositories[typeof(T)] as IRepository<T>;

        //    var repository = new Repository<T>()
        //}

        public async Task RollBackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //public IRepository<TEntity, TContext> GetRepository<TRepoEntity>() where TRepoEntity : class, IBaseEntity, TEntity
        //{
        //    if (_repositories.ContainsKey(typeof(TRepoEntity)))
        //        return _repositories[typeof(TRepoEntity)] as IRepository<TEntity, TContext>;

        //    var repository = new Repository<TRepoEntity, TContext>(_context);
        //    _repositories.Add(typeof(TRepoEntity), repository);
        //    return repository as IRepository<TEntity, TContext>;
        //}
    }
}