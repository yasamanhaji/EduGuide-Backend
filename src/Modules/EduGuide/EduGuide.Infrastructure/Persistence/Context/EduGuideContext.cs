using Base.Application.Contracts;
using Base.Infrastructure.Persistence.Context;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduGuide.Infrastructure.Persistence.Context
{
    public class EduGuideContext(DbContextOptions<EduGuideContext> options, IJwtManager jwtManger) 
        : BaseContext(options, typeof(User).Assembly, typeof(EduGuideContext).Assembly, jwtManger)
        , IEduGuideContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.SeedAdmin();
        }
    }
}