using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Enums;
using MediatR;

namespace EduGuide.Application.CQRS.RequestCounselors
{
    public class RequestCancelCommand
        : IRequest<Result>
    {
    }

    public class RequestCancelCommandHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager)
        : IRequestHandler<RequestCancelCommand, Result>
    {
        public async Task<Result> Handle(RequestCancelCommand request, CancellationToken cancellationToken)
        {
            var userId = jwtManager.GetUserId();

            var requestCounselor = await uow.RequestCounselors
                .FirstOrDefaultAsync(s => s.Student.UserId == userId && (s.status == RequestStatus.Requested || s.status == RequestStatus.ApprovedByCounselor
                || s.status == RequestStatus.Settlement));

            if (requestCounselor == null)
                throw new Exception("مشاوره‌ای برای کنسل کردن، وجود ندارد!");
            if (requestCounselor.status != RequestStatus.Requested && requestCounselor.status != RequestStatus.ApprovedByCounselor
                && requestCounselor.status != RequestStatus.Settlement)
                throw new Exception("این درخواست قابل کنسل نیست!");

            var payemnt = await uow.Payments.FirstOrDefaultAsync(s => s.RequestCounselorId == requestCounselor.Id);
            if (payemnt != null)
                payemnt.IsDeleted = true;
            requestCounselor.status = RequestStatus.Canceled;

            await uow.CommitAsync();
            return Result.Success(true);
        }
    }
}