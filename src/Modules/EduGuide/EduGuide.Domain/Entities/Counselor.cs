using Base.Domain.Entities.Common;

namespace EduGuide.Domain.Entities
{
    public class Counselor : BaseEntity
    {
        public string NationalCode { get; set; }
        public string CardNum { get; set; }
        public string AboutMe { get; set; }
        public string ProfilePicName { get; set; }

        public long CounselorRecruitmentId { get; set; }
        public CounselorRecruitment CounselorRecruitment { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
        public double Score { get; set; }
        public int ScoreCount { get; set; }
    }
}