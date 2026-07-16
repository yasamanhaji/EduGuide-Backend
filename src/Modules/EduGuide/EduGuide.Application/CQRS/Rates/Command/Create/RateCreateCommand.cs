using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace EduGuide.Application.CQRS.Rates
{
    public class RateCreateCommand
        : IRequest<Result>
    {
        public long RequestCounselorId { get; set; }
        public int Score { get; set; }
    }

    public class RateCreateCommandHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager)
    : IRequestHandler<RateCreateCommand, Result>
    {
        public async Task<Result> Handle(RateCreateCommand request, CancellationToken cancellationToken)
        {
            var userId = jwtManager.GetUserId();

            var requestCounselor = await uow.RequestCounselors.FirstOrDefaultAsync(
                x => x.Id == request.RequestCounselorId,
                includes: x => x.Include(x => x.Counselor).Include(x => x.Student)
                );

            if (requestCounselor == null)
                throw new Exception("رکورد موردنظر یافت نشد!");

            if (requestCounselor.Student.UserId != userId || requestCounselor.status != RequestStatus.Settlement 
                || Math.Abs((requestCounselor.EndDate.Value - DateTime.Now).Days) > 5)
                throw new Exception("شما برای این مشاوره نمی‌توانید امتیاز ثبت کنید!");

            var existingRate = await uow.Rates.LastOrDefaultAsync(x => x.RequestCounselorId == request.RequestCounselorId, orderBy: x => x.OrderBy("id asc"));
            if (existingRate != null)
            {
                if (existingRate.CreateDate > requestCounselor.EndDate.Value.AddDays(-6) && existingRate.CreateDate < requestCounselor.EndDate.Value.AddDays(6))
                    throw new Exception("شما قبلا امتیاز داده‌اید!");
            }
            
            var rate = new Rate()
            {
                Score = request.Score,
                RequestCounselorId = request.RequestCounselorId,
                UserId = userId.Value
            };
            await uow.Rates.AddAsync(rate);
            await uow.CommitAsync();

            requestCounselor.Counselor.Score = Math.Round(
                (requestCounselor.Counselor.Score * requestCounselor.Counselor.ScoreCount + request.Score) /
                (requestCounselor.Counselor.ScoreCount + 1),
                1,
                MidpointRounding.AwayFromZero);
            requestCounselor.Counselor.ScoreCount += 1;
            await uow.CommitAsync();

            return Result.Success();
        }
    }
}