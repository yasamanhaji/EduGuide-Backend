using Base.Utilities.Extensions;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Counselors
{
    public class CounselorGetByIdDTO
    {
        public string AboutMe { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string UniMajor { get; set; }
        public string HsMajorTitle { get; set; }
        public string CountryRanking { get; set; }
        public string EntranceExamYear { get; set; }
        public string UniName { get; set; }
        public int EmploymentDuration { get; set; }
        public long? StudentCounselorId { get; set; }
        public RequestStatus? RequestStatus { get; set; }
        public double Rate { get; set; }
        public string PicName { get; set; }
        public string PicUrl { get; set; }

        public static Expression<Func<Counselor, CounselorGetByIdDTO>> Selector =>
            model => new CounselorGetByIdDTO
            {
                AboutMe = model.AboutMe,
                FullName = model.CounselorRecruitment.FirstName + ' ' + model.CounselorRecruitment.LastName,
                Email = model.CounselorRecruitment.Email,
                PhoneNumber = model.CounselorRecruitment.PhoneNumber,
                Province = model.CounselorRecruitment.Province,
                UniMajor = model.CounselorRecruitment.UniMajor,
                CountryRanking = model.CounselorRecruitment.CountryRanking,
                EntranceExamYear = model.CounselorRecruitment.EntranceExamYear,
                UniName = model.CounselorRecruitment.UniName,
                HsMajorTitle = model.CounselorRecruitment.HsMajor.GetDescription(),
                PicName = model.ProfilePicName,
                Rate = model.Score,
                EmploymentDuration = (DateTime.Now - model.CreateDate).Days / 365 == 0 ? 1 : (DateTime.Now - model.CreateDate).Days / 365,
            };
    }
}
