using Base.Infrastructure.Implementation;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;

namespace EduGuide.Infrastructure.Implementation.Repositories
{
    public class CounselorRecruitmentRepository : Repository<CounselorRecruitment, IEduGuideContext>, ICounselorRecruitmentRepository
    {
        public CounselorRecruitmentRepository(IEduGuideContext dbContext) : base(dbContext)
        {
        }
    }
}
