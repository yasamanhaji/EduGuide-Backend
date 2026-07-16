using Base.Domain.Entities.Common;
using EduGuide.Domain.Enums;

namespace EduGuide.Domain.Entities
{
    public class CounselorRecruitment:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string UniMajor { get; set; }
        public HsMajor HsMajor { get; set; }
        public string CountryRanking { get; set; }
        public string EntranceExamYear { get; set; }
        public string UniName { get; set; }
        public string Employmenthistory { get; set; }
        public bool Approved { get; set; }
        public string SudentCardPicName { get; set; }
    }
}