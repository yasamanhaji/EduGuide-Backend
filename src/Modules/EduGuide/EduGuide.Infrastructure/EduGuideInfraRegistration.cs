using Base.Infrastructure;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Infrastructure.Implementation.Repositories;
using EduGuide.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EduGuide.Infrastructure
{
    public static class EduGuideInfraRegistration
    {
        public static void RegisterEduGuideInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EduGuideContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IEduGuideContext, EduGuideContext>();
            services.AddRepositories(Assembly.GetExecutingAssembly());
            services.AddScoped<IEduGuideUnitOfWork, EduGuideUnitOfWork>();
            //services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

        }
    }
}