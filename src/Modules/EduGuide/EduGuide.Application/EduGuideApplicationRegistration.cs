using Base.Application;
using EduGuide.Application.Contracts.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Hosting;


namespace EduGuide.Application
{
    public static class EduGuideApplicationRegistration
    {
        public static void RegisterEduGuideApplication(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            var services = builder.Services;

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddFluentValidation(Assembly.GetExecutingAssembly());
            services.AddHostedService<RequestCounselorCleanupService>();

        }
    }
}