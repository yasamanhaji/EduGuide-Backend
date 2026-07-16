using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using MediatR;

namespace EduGuide.Application.CQRS.RequestCounselors.Command.Approve
{
    public class RequestApproveCommand : IRequest<Result<bool>>
    {
        public List<long> Id { get; set; }
    }
    public class RequestApproveCommandHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager) : IRequestHandler<RequestApproveCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(RequestApproveCommand request, CancellationToken cancellationToken)
        {

            var userId = jwtManager.GetUserId();

            foreach (var Id in request.Id)
            {
                var requestCounselor = await uow.RequestCounselors
                    .FirstOrDefaultAsync(s => s.Id == Id && s.Counselor.UserId == userId);

                if (requestCounselor == null)
                {
                    throw new Exception("رکوردی یافت نشد ");
                }

                if (requestCounselor.status != RequestStatus.Requested)
                {
                    throw new Exception("این دانش اموز درخواست نداده ");
                }
                requestCounselor.status = RequestStatus.ApprovedByCounselor;

                var payment = new Payment()
                {
                    Amount = 100,
                    IsPaid = false,
                    CounselingDuration = 1,
                    StudentId = requestCounselor.StudentId,
                    CounselorId = requestCounselor.CounselorId,
                    RequestCounselor = requestCounselor,
                };
                await uow.Payments.AddAsync(payment);
            }
            await uow.CommitAsync();
            return Result.Success(true);
        }
    }
}