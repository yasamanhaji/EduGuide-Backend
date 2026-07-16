using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using Base.Application.Exceptions;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using MediatR;

namespace EduGuide.Application.CQRS.RequestCounselors.Command.Create
{
    public class RequestCreateCommand : IRequest<Result<bool>>
    {
        public long id { get; set; }
    }
    public class RequestCreateCommandHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager) : IRequestHandler<RequestCreateCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(RequestCreateCommand request, CancellationToken cancellationToken)
        {
            var userId = jwtManager.GetUserId();
            var student = await uow.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
                throw new Exception("رکورد موردنظر یافت نشد!");

            var studentId = student.Id;
            var condition = await uow.RequestCounselors.AnyAsync
                  (
                  s => s.StudentId == studentId &&
                  (s.status == RequestStatus.Requested || s.status == RequestStatus.Settlement || s.status == RequestStatus.ApprovedByCounselor)
                  );

            if (condition)
            {
                throw new RequestCounselorException("نمیتوانید بیش از یک درخواست بدهید ");
            }
            if (!student.IsProfileComplete)
            {
                throw new Exception("ابتدا پروفایل کاربری خود را کامل کنید!");
            }
            if (await uow.Payments.AnyAsync(x => x.StudentId == studentId && x.IsPaid == false))
                throw new Exception("ابتدا صورتحساب‌های خود را تسویه کنید!");


            RequestCounselor requestCounselor = new RequestCounselor()
            {

                StudentId = studentId,
                CounselorId = request.id,
                status = RequestStatus.Requested
            };
            await uow.RequestCounselors.AddAsync(requestCounselor);
            await uow.CommitAsync();

            return Result<bool>.Success(true);
        }
    }
}