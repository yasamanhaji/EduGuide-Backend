using Base.Domain.Entities.Common;

namespace EduGuide.Domain.Entities
{
    public class Rate : BaseEntity
    {
        public int Score { get; set; }
        public long RequestCounselorId { get; set; }
        public RequestCounselor RequestCounselor { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}