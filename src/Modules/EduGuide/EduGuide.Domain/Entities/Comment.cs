using Base.Domain.Entities.Common;

namespace EduGuide.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public bool Approved { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
        public long CounselorId { get; set; }
        public Counselor Counselor { get; set; }
    }
}