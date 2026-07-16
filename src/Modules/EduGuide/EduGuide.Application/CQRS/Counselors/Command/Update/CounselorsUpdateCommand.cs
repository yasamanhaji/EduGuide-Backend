using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EduGuide.Application.CQRS.Counselors.Command.Update
{
    public class CounselorsUpdateCommand : IRequest<Result<bool>>
    {
        public long Id { get; set; }
        public string Province { get; set; }
        public string AboutMe { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Employmenthistory { get; set; }
        public IFormFile StudentCardPic { get; set; }
        public IFormFile ProfilePic { get; set; }
    }
    public class CounselorsUpdateCommandHandler(IGenericRepository<Counselor, IEduGuideContext> genericRepository, IMinIoService minIoService,
        IJwtManager jwtManager)
        : IRequestHandler<CounselorsUpdateCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(CounselorsUpdateCommand request, CancellationToken cancellationToken)
        {
            var counselorrrr = await genericRepository.Repository
                .FirstOrDefaultAsync(
                c => c.Id == request.Id,
                query => query
                .Include(c => c.CounselorRecruitment)
                .Include(c => c.User)
                );


            if (counselorrrr == null)
                throw new Exception("رکورد موردنظر یافت نشد!");

            counselorrrr.User.Email = string.IsNullOrEmpty(request.Email) ? counselorrrr.User.Email : request.Email;
            counselorrrr.CounselorRecruitment.PhoneNumber = string.IsNullOrEmpty(request.PhoneNumber) ? counselorrrr.CounselorRecruitment.PhoneNumber : request.PhoneNumber;
            counselorrrr.CounselorRecruitment.Employmenthistory = string.IsNullOrEmpty(request.Employmenthistory) ? counselorrrr.CounselorRecruitment.Employmenthistory : request.Employmenthistory;
            counselorrrr.AboutMe = string.IsNullOrEmpty(request.AboutMe) ? counselorrrr.AboutMe : request.AboutMe;
            counselorrrr.CounselorRecruitment.Province = request.Province;

            if (request.StudentCardPic != null)
            {
                await minIoService.UploadFile(request.StudentCardPic, "CounselorRecruitment/");
                counselorrrr.CounselorRecruitment.SudentCardPicName = request.StudentCardPic.FileName;
            }

            if (request.ProfilePic != null)
            {
                await minIoService.UploadFile(request.ProfilePic, $"Counselor/Profile/{jwtManager.GetUserName()}/");
                counselorrrr.ProfilePicName = request.ProfilePic.FileName;
            }

            await genericRepository.CommitAsync();
            return Result<bool>.Success(true);
        }
    }
}