using Base.Domain.Entities.Common;

namespace Base.Application.Contracts
{
    public interface IGenericRepository<TEntity, TContext> : IUnitOfWork<TContext>
        where TContext : IDbContext
        where TEntity : IBaseEntity
    {
        IRepository<TEntity, TContext> Repository { get; }
    }
}