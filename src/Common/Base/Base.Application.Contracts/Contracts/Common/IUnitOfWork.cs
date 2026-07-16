namespace Base.Application.Contracts
{
    public interface IUnitOfWork<TContext> : IDisposable
        where TContext : IDbContext
    {
        //IRepository<TEntity, TContext> GetRepository<TRepoEntity>() where TRepoEntity : class, IBaseEntity, TEntity;
        Task<int> SaveChangeAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();
    }
}
