using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using MediatR;

namespace EduGuide.Application.CQRS.RequestCounselors
{
    public class RequestExtendCommand
        : IRequest<Result>
    {
        public long Id { get; set; }
    }

    public class RequestExtendCommandHadler(IEduGuideUnitOfWork uow)
        : IRequestHandler<RequestExtendCommand, Result>
    {
        public async Task<Result> Handle(RequestExtendCommand request, CancellationToken cancellationToken)
        {
            var entity = await uow.RequestCounselors.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (entity == null)
                throw new Exception("رکورد موردنظر یافت نشد!");

            if (entity.status != Domain.Enums.RequestStatus.Settlement || 
                Math.Abs((entity.EndDate.Value - DateTime.Now).Days) > 5)
                throw new Exception("این مشاوره قابل تمدید نمی‌باشد!");

            var payment = new Payment()
            {
                Amount = 100,
                IsPaid = true,
                CounselingDuration = 1,
                StudentId = entity.StudentId,
                CounselorId = entity.CounselorId,
                RequestCounselor = entity,
            };
            await uow.Payments.AddAsync(payment);

            entity.EndDate = entity.EndDate.Value.AddDays(30);

            await uow.CommitAsync();
            return Result.Success();
        }
    }
}