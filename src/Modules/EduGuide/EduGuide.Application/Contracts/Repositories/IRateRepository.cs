using Base.Application.Contracts;
using EduGuide.Domain.Entities;

namespace EduGuide.Application.Contracts.Repositories
{
    public interface IRateRepository : IRepository<Rate, IEduGuideContext>
    {
    }
}