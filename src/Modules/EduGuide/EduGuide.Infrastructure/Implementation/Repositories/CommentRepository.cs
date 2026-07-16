using Base.Infrastructure.Implementation;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;

namespace EduGuide.Infrastructure.Implementation.Repositories
{
    public class CommentRepository : Repository<Comment, IEduGuideContext>, ICommentRepository
    {
        public CommentRepository(IEduGuideContext dbContext) : base(dbContext)
        {
        }
    }
}
