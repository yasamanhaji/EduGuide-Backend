using Base.Infrastructure.Implementation;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;

namespace EduGuide.Infrastructure.Implementation.Repositories
{
    public class RateRepository : Repository<Rate, IEduGuideContext>, IRateRepository
    {
        public RateRepository(IEduGuideContext dbContext) : base(dbContext)
        {
        }
    }
}