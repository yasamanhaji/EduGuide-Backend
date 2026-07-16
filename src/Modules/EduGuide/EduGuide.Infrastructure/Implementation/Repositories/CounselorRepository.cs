using Base.Infrastructure.Implementation;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;

namespace EduGuide.Infrastructure.Implementation.Repositories
{
    public class CounselorRepository : Repository<Counselor, IEduGuideContext>, ICounselorRepository
    {
        public CounselorRepository(IEduGuideContext dbContext) : base(dbContext)
        {
        }
    }
}
