using Base.Infrastructure.Implementation;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;

namespace EduGuide.Infrastructure.Implementation.Repositories
{
    public class UserRepository : Repository<User, IEduGuideContext>, IUserRepository
    {
        public UserRepository(IEduGuideContext dbContext) : base(dbContext)
        {
        }
    }
}
