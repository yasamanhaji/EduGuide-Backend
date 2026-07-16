using Base.Utilities.Extensions;
using EduGuide.Domain.Entities;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Counselors
{
    public class OurCounselorGetListDTO
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UniMajor { get; set; }
        public string HsMajor { get; set; }
        public string UniName { get; set; }
        public string EntranceExamYear { get; set; }
        public int EmploymentDuration { get; set; }
        public double Rate { get; set; }
        public string PicName { get; set; }
        public string PicUrl { get; set; }

        public static Expression<Func<Counselor, OurCounselorGetListDTO>> Selector =>
            model => new OurCounselorGetListDTO
            {
                Id = model.Id,
                FullName = model.CounselorRecruitment.FirstName + ' ' + model.CounselorRecruitment.LastName,
                UniMajor = model.CounselorRecruitment.UniMajor,
                UniName = model.CounselorRecruitment.UniName,
                EntranceExamYear = model.CounselorRecruitment.EntranceExamYear,
                HsMajor = model.CounselorRecruitment.HsMajor.GetDescription(),
                EmploymentDuration = (DateTime.Now - model.CreateDate).Days / 365 == 0 ? 1 : (DateTime.Now - model.CreateDate).Days / 365,
                PicName = model.ProfilePicName,
                Rate = model.Score
            };
    }
}
