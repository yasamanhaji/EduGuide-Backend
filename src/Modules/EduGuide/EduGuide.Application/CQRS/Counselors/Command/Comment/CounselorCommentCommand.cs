using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using MediatR;

namespace EduGuide.Application.CQRS.Counselors
{
    public class CounselorCommentCommand
        : IRequest<Result>
    {
        public long CounselorId { get; set; }
        public string Text { get; set; }
    }

    public class CounselorCommentCommandHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager)
        : IRequestHandler<CounselorCommentCommand, Result>
    {
        public async Task<Result> Handle(CounselorCommentCommand request, CancellationToken cancellationToken)
        {
            var userId = jwtManager.GetUserId();

            if (!await uow.RequestCounselors.AnyAsync(x => x.Student.UserId == userId && x.CounselorId == request.CounselorId && 
            (x.status == RequestStatus.Settlement || x.status == RequestStatus.Ended || (x.status == RequestStatus.Canceled && x.StartDate != null))))
            {
                throw new Exception("شما برای این مشاور نمی‌توانید کامنت ثبت کنید!");
            }

            var comment = new Comment()
            {
                Text = request.Text,
                UserId = userId.Value,
                CounselorId = request.CounselorId,
            };
            await uow.Comments.AddAsync(comment);
            await uow.CommitAsync();

            return Result.Success();
        }
    }
}