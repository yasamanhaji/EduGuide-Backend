using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using MediatR;

namespace EduGuide.Application.CQRS.Counselors
{
    public class CommentAprroveCommand
        : IRequest<Result>
    {
        public List<long> CommentIds { get; set; }
    }

    public class CommentAprroveCommandHandler(IEduGuideUnitOfWork uow)
        : IRequestHandler<CommentAprroveCommand, Result>
    {
        public async Task<Result> Handle(CommentAprroveCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.CommentIds)
            {
                var comment = await uow.Comments.FirstOrDefaultAsync(x => x.Id == id);
                if (comment == null)
                    throw new Exception("رکورد موردنظر یافت نشد!");

                comment.Approved = true;
                await uow.CommitAsync();
            }

            return Result.Success();
        }
    }
}