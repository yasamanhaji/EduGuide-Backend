using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Enums;
using MediatR;

namespace EduGuide.Application.CQRS.RequestCounselors.Command.Reject
{
    public class RequestCounselorRejectCommand 
        : IRequest<Result>
    {
        public long Id { get; set; }
    }

    public class RequestCounselorRejectCommandHandler(IJwtManager jwtManager, IEduGuideUnitOfWork uow)
        : IRequestHandler<RequestCounselorRejectCommand, Result>
    {
        public async Task<Result> Handle(RequestCounselorRejectCommand request, CancellationToken cancellationToken)
        {
            var userId = jwtManager.GetUserId();

            var requestCounselor = await uow.RequestCounselors
                .FirstOrDefaultAsync(s => s.Id == request.Id && s.Counselor.UserId == userId);

            if (requestCounselor == null)
                throw new Exception("رکورد موردنظر یافت نشد!");
            if (requestCounselor.status != RequestStatus.Requested)
                throw new Exception("این درخواست قابل رد نیست!");

            requestCounselor.status = RequestStatus.Rejected;

            await uow.CommitAsync();
            return Result.Success(true);
        }
    }
}