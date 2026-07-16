//using EduGuide.Application.Contracts.Repositories;

//public class BackgroundTaskRunner
//{
//    private readonly RequestCounselorCleanupService _cleanupService;

//    public BackgroundTaskRunner(IEduGuideUnitOfWork unitOfWork)
//    {
//        _cleanupService = new RequestCounselorCleanupService(unitOfWork);
//    }

//    public void Start()
//    {
//        Task.Run(() => _cleanupService.RunCleanupLoopAsync(CancellationToken.None));
//    }
//}
