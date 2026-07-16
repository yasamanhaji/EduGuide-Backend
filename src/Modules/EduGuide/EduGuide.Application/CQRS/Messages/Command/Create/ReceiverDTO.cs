namespace EduGuide.Application.CQRS.Messages
{ 
    public class ReceiverDTO
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public long MessageId { get; set; }
    }
}
