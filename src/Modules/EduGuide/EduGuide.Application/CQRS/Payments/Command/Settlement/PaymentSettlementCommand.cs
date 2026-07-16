using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduGuide.Application.CQRS.Payments.Command.Settlement
{
    public class PaymentSettlementCommand
        : IRequest<Result>
    {
        public long Id { get; set; }
    }

    public class PaymentSettlementCommandHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager)
        : IRequestHandler<PaymentSettlementCommand, Result>
    {
        public async Task<Result> Handle(PaymentSettlementCommand request, CancellationToken cancellationToken)
        {
            var payment = await uow.Payments.FirstOrDefaultAsync(
                x => x.Id == request.Id,
                x => x.Include(x => x.Student).Include(x => x.RequestCounselor));
            if (payment == null)
                throw new Exception("رکورد موردنظر یافت نشد!");
            if (payment.Student.UserId != jwtManager.GetUserId())
                throw new Exception("صورتحساب متعلق به شما نیست!");

            payment.IsPaid = true;
            payment.RequestCounselor.status = Domain.Enums.RequestStatus.Settlement;

            if (payment.RequestCounselor.StartDate == null)
                payment.RequestCounselor.StartDate = DateTime.Now;

            if (payment.RequestCounselor.EndDate == null)
                payment.RequestCounselor.EndDate = DateTime.Now.AddDays(30);
            else
                payment.RequestCounselor.EndDate = payment.RequestCounselor.EndDate.Value.AddDays(30);

            await uow.CommitAsync();
            return Result.Success();
        }
    }
}