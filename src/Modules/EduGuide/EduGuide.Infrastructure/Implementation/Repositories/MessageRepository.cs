using Base.Infrastructure.Implementation;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;

namespace EduGuide.Infrastructure.Implementation.Repositories
{
    public class MessageRepository : Repository<Message, IEduGuideContext>, IMessageRepository
    {
        public MessageRepository(IEduGuideContext dbContext) : base(dbContext)
        {
        }
    }
}