using Base.Utilities.Extensions;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.CounselorRecruitments
{
    public class CounselorRecruitmentGetListDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string UniMajor { get; set; }
        public HsMajor HsMajor { get; set; }
        public string HsMajorTitle => HsMajor.GetDescription();
        public string CountryRanking { get; set; }
        public string EntranceExamYear { get; set; }
        public string UniName { get; set; }
        public string Employmenthistory { get; set; }
        public bool Approved { get; set; }
        public string StudentCardPicName { get; set; }
        public string FileUrl { get; set; }
        public static Expression<Func<CounselorRecruitment, CounselorRecruitmentGetListDTO>> Selector
            => model => new CounselorRecruitmentGetListDTO()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Province = model.Province,
                CountryRanking = model.CountryRanking,
                EntranceExamYear = model.EntranceExamYear,
                UniName = model.UniName,
                Employmenthistory = model.Employmenthistory,
                Approved = model.Approved,
                StudentCardPicName = model.SudentCardPicName,
                UniMajor = model.UniMajor,
                HsMajor = model.HsMajor,
            };
    }
}