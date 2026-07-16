using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EduGuide.Application.CQRS.CounselorRecruitments.Command.Update
{
    public class CounselorRecruitmentUpdateCommand : IRequest<Result<bool>>
    {
        public long CounselorRecruitmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string CountryRanking { get; set; }
        public string EntranceExamYear { get; set; }
        public string UniName { get; set; }
        public HsMajor? HsMajor { get; set; }
        public string UniMajor { get; set; }
        public string Employmenthistory { get; set; }
        public IFormFile StudentCardPic { get; set; }
    }

    public class CounselorRecruitmentUpdateCommandHandler(IGenericRepository<CounselorRecruitment, IEduGuideContext> genericRepository, IMinIoService minIoService)
        : IRequestHandler<CounselorRecruitmentUpdateCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(CounselorRecruitmentUpdateCommand request, CancellationToken cancellationToken)
        {
            var counselorRecruitment = await genericRepository.Repository.GetByIdAsync(request.CounselorRecruitmentId);
            if (counselorRecruitment == null)
                throw new Exception("رکورد موردنظر یافت نشد!");

            counselorRecruitment.FirstName = string.IsNullOrEmpty(request.FirstName) ? counselorRecruitment.FirstName : request.FirstName;
            counselorRecruitment.LastName = string.IsNullOrEmpty(request.LastName) ? counselorRecruitment.LastName : request.LastName;
            counselorRecruitment.Email = string.IsNullOrEmpty(request.Email) ? counselorRecruitment.Email : request.Email;
            counselorRecruitment.PhoneNumber = string.IsNullOrEmpty(request.PhoneNumber) ? counselorRecruitment.PhoneNumber : request.PhoneNumber;
            counselorRecruitment.Province = string.IsNullOrEmpty(request.Province) ? counselorRecruitment.Province : request.Province;
            counselorRecruitment.HsMajor = request.HsMajor == null ? counselorRecruitment.HsMajor : request.HsMajor.Value;
            counselorRecruitment.CountryRanking = string.IsNullOrEmpty(request.CountryRanking) ? counselorRecruitment.CountryRanking : request.CountryRanking;
            counselorRecruitment.EntranceExamYear = string.IsNullOrEmpty(request.EntranceExamYear) ? counselorRecruitment.EntranceExamYear : request.EntranceExamYear;
            counselorRecruitment.UniMajor = string.IsNullOrEmpty(request.UniMajor) ? counselorRecruitment.UniMajor : request.UniMajor;
            counselorRecruitment.UniName = string.IsNullOrEmpty(request.UniName) ? counselorRecruitment.UniName : request.UniName;
            counselorRecruitment.Employmenthistory = string.IsNullOrEmpty(request.Employmenthistory) ? counselorRecruitment.Employmenthistory : request.Employmenthistory;
            counselorRecruitment.SudentCardPicName = request.StudentCardPic.Name == null ? counselorRecruitment.SudentCardPicName : request.StudentCardPic.Name;

            if (request.StudentCardPic != null)
                await minIoService.UploadFile(request.StudentCardPic, "CounselorRecruitment/");

            await genericRepository.CommitAsync();

            return Result<bool>.Success(true);
        }   
    }
}
