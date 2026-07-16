using Base.Utilities.Extensions;
using EduGuide.Domain.Entities;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Counselors
{
    public class CounselorsProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string UniMajor { get; set; }
        public string HsMajorTitle { get; set; }
        public string EntranceExamYear { get; set; }
        public string CountryRanking { get; set; }
        public string UniName { get; set; }
        public string Employmenthistory { get; set; }
        public string AboutMe { get; set; }
        public string StudentCardPicName { get; set; }
        public string StudenCardPiceUrl { get; set; }
        public string ProfilePicName { get; set; }
        public string ProfilePicUrl { get; set; }

        public static Expression<Func<Counselor, CounselorsProfileDto>> Selector
            => model => new CounselorsProfileDto()
            {
                Id = model.Id,
                FirstName = model.CounselorRecruitment.FirstName,
                LastName = model.CounselorRecruitment.LastName,
                Email = model.CounselorRecruitment.Email,
                PhoneNumber = model.CounselorRecruitment.PhoneNumber,
                Province = model.CounselorRecruitment.Province,
                UniMajor = model.CounselorRecruitment.UniMajor,
                CountryRanking = model.CounselorRecruitment.CountryRanking,
                UniName = model.CounselorRecruitment.UniName,
                Employmenthistory = model.CounselorRecruitment.Employmenthistory,
                AboutMe = model.AboutMe,
                StudentCardPicName = model.CounselorRecruitment.SudentCardPicName,
                HsMajorTitle = model.CounselorRecruitment.HsMajor.GetDescription(),
                ProfilePicName = model.ProfilePicName,
                EntranceExamYear = model.CounselorRecruitment.EntranceExamYear,
            };

    }
}
