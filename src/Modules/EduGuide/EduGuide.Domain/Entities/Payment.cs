using Base.Domain.Entities.Common;

namespace EduGuide.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public int CounselingDuration { get; set; }

        public long StudentId { get; set; }
        public Student Student { get; set; }
        public long CounselorId { get; set; }
        public Counselor Counselor { get; set; }
        public long RequestCounselorId { get; set; }
        public RequestCounselor RequestCounselor { get; set; }
    }
}