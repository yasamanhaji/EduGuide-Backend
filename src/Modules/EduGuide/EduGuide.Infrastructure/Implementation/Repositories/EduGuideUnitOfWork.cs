using Base.Infrastructure.Implementation;
using EduGuide.Application.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EduGuide.Infrastructure.Implementation.Repositories
{
    public class EduGuideUnitOfWork : UnitOfWork<IEduGuideContext>, IEduGuideUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        public EduGuideUnitOfWork(IEduGuideContext context, IServiceProvider serviceProvider) : base(context)
        {
            _serviceProvider = serviceProvider;
        }

        public IUserRepository Users => _serviceProvider.GetService<IUserRepository>();
        public ICounselorRecruitmentRepository CounselorRecruitments => _serviceProvider.GetService<ICounselorRecruitmentRepository>();
        public ICounselorRepository Counselors => _serviceProvider.GetService<ICounselorRepository>();
        public IStudentRepository Students => _serviceProvider.GetService<IStudentRepository>();
        public IRequestCounselorRepository RequestCounselors=> _serviceProvider.GetService<IRequestCounselorRepository>();
        public IPaymentRepository Payments => _serviceProvider.GetService<IPaymentRepository>();
        public ICommentRepository Comments => _serviceProvider.GetService<ICommentRepository>();
        public IRateRepository Rates => _serviceProvider.GetService<IRateRepository>();
        public IMessageRepository Messages => _serviceProvider.GetService<IMessageRepository>();
    }
}