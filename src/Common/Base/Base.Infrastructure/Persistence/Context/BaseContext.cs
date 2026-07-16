using Base.Application.Contracts;
using Base.Domain.Entities.Common;
using Base.Domain.Interfaces;
using Base.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Base.Infrastructure.Persistence.Context
{
    public class BaseContext(DbContextOptions options, Assembly domainAssembly, Assembly infraAssembly, IJwtManager jwtManager)
        : DbContext(options)
        , IDbContext
    {
        private readonly Assembly _domainAssembly = domainAssembly;
        private readonly Assembly _InfraAssembly = infraAssembly;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(_InfraAssembly);
            modelBuilder.RegisterEntites<BaseEntity>(_domainAssembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
                    {
                        relationship.DeleteBehavior = DeleteBehavior.Restrict;
                    }
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var isDeletedProperty = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
                    var filter = Expression.Lambda(Expression.Equal(isDeletedProperty, Expression.Constant(false)), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditFields()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var userId = jwtManager?.GetUserId();
            foreach (var entry in entries)
            {
                var entity = (IBaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = userId;
                }

                entity.LastModifyDate = DateTime.Now;
                entity.LastModifyBy = userId;
            }
        }
    }
}