using EduGuide.Domain.Entities;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Messages
{
    public class ContactMessagesDTO
    {
        public long Id { get; set; }
        public long ReceiverId { get; set; }
        public long SenderId { get; set; }
        public bool Seen { get; set; }
        public string Text { get; set; }
        public string SendDate { get; set; }
        public bool IsFile { get; set; }
        public string FileUrl { get; set; }
        public string FilePath { get; set; }

        //public static Expression<Func<Message, ContactMessagesDTO>> Selector =>
        //    model => new ContactMessagesDTO
        //    {
        //        Id = model.Id,
        //        ReceiverId = model.ReceiverId,
        //        Text = model.Text,
        //        SendDate = model.CreateDate,
        //    };
    }
}
