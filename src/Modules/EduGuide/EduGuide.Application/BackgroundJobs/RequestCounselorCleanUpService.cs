using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class RequestCounselorCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RequestCounselorCleanupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IEduGuideUnitOfWork>();

                var cutoff = DateTime.Now.AddDays(-5); // local t
                var staleRequests = await unitOfWork.RequestCounselors
                    .GetAsync(r => r.status == RequestStatus.Settlement && r.EndDate < cutoff);

                foreach (var request in staleRequests)
                {
                    request.status = RequestStatus.Ended;
                }

                if (staleRequests.Any())
                {
                    await unitOfWork.CommitAsync();
                }
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);


        }
    }
}
