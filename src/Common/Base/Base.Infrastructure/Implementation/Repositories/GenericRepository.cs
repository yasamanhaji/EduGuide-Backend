using Base.Application.Contracts;
using Base.Domain.Entities.Common;

namespace Base.Infrastructure.Implementation
{
    public class GenericRepository<TEntity, TContext> : UnitOfWork<TContext>, IGenericRepository<TEntity, TContext>
        where TEntity : class, IBaseEntity
        where TContext : IDbContext
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public GenericRepository(TContext context) : base(context)
        {
        }

        public IRepository<TEntity, TContext> Repository => GetRepository();

        private IRepository<TEntity, TContext> GetRepository()
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IRepository<TEntity, TContext>;

            var repository = new Repository<TEntity, TContext>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }
    }
}
