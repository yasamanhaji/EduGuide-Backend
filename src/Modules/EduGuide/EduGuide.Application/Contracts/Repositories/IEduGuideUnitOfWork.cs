using Base.Application.Contracts;

namespace EduGuide.Application.Contracts.Repositories
{
    public interface IEduGuideUnitOfWork : IUnitOfWork<IEduGuideContext>, IDisposable
    {
        IUserRepository Users { get; }
        ICounselorRecruitmentRepository CounselorRecruitments { get; }
        ICounselorRepository Counselors { get; }
        IStudentRepository Students { get; }
        IRequestCounselorRepository RequestCounselors { get; }
        IPaymentRepository Payments { get; }
        ICommentRepository Comments { get; }
        IRateRepository Rates { get; }
        IMessageRepository Messages { get; }
    }
}