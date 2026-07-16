using Base.Domain.Entities.Common;

namespace EduGuide.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }
        public bool Seen { get; set; }
        public bool IsFile { get; set; }
        public string Path { get; set; }

        public long SenderId { get; set; }
        public User Sender { get; set; }

        public long ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
