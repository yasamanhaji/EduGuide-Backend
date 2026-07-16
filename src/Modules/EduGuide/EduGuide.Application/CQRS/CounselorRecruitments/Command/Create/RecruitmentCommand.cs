using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EduGuide.Application.CQRS.CounselorRecruitments
{
    public class RecruitmentCommand : IRequest<Result<bool>>
    {
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
    public class RecriutmentCommandHandler(IGenericRepository<CounselorRecruitment, IEduGuideContext> uow, IMinIoService minIoService)
         : IRequestHandler<RecruitmentCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(RecruitmentCommand request, CancellationToken cancellationToken)
        {
            var counselorRecruitments = new CounselorRecruitment()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Province = request.Province,
                HsMajor = request.HsMajor.Value,
                CountryRanking = request.CountryRanking,
                EntranceExamYear = request.EntranceExamYear,
                UniName = request.UniName,
                Employmenthistory = request.Employmenthistory,
                SudentCardPicName = request.StudentCardPic.FileName,
                UniMajor = request.UniMajor,
            };

            await minIoService.UploadFile(request.StudentCardPic, "CounselorRecruitment/");

            await uow.Repository.AddAsync(counselorRecruitments);
            await uow.CommitAsync();

            return Result<bool>.Success(true);
        }
    }
}