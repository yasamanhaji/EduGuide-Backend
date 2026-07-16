using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EduGuide.Application.CQRS.CounselorRecruitments
{
    public class CounselorApproveCommand : IRequest<Result<bool>>
    {
        public List<long> CounselorRecruitmentIds { get; set; }
    }

    public class CounselorApproveCommandHandler(IEduGuideUnitOfWork uow)
        : IRequestHandler<CounselorApproveCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(CounselorApproveCommand request, CancellationToken cancellationToken)
        {
            foreach (var counselorRecruitmentId in request.CounselorRecruitmentIds)
            {
                var counselorRecruitment = await uow.CounselorRecruitments.GetByIdAsync(counselorRecruitmentId);
                if (counselorRecruitment == null)
                    throw new Exception("رکورد موردنظر یافت نشد!");

                counselorRecruitment.Approved = true;

                var user = new User()
                {
                    FirstName = counselorRecruitment.FirstName,
                    LastName = counselorRecruitment.LastName,
                    Email = counselorRecruitment.Email,
                    Role = "Counselor",
                    UserName = counselorRecruitment.FirstName + ' ' + counselorRecruitment.LastName
                };
                user.PasswordHash = new PasswordHasher<User>().HashPassword(user, GetDomainFromEmail(user.Email));
                var counselor = new Counselor()
                {
                    CounselorRecruitment = counselorRecruitment,
                    User = user
                };

                await uow.Users.AddAsync(user);
                await uow.Counselors.AddAsync(counselor);
                await uow.CommitAsync();
            }

            return Result<bool>.Success(true);
        }

        private string GetDomainFromEmail(string email)
        {
            int atIndex = email.IndexOf('@');
            var res = email.Substring(0, atIndex);
            return res;
        }
    }
}