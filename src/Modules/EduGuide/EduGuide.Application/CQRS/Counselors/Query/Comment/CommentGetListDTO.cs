using EduGuide.Domain.Entities;
using FarsiLibrary.Utils;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Counselors
{
    public class CommentGetListDTO
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public bool Approved { get; set; }
        public string StudentName { get; set; }
        public string CounselorName { get; set; }
        public string CreateDate { get; set; }

        public static Expression<Func<Comment, CommentGetListDTO>> Selector =>
            model => new CommentGetListDTO
            {
                Id = model.Id,
                Text = model.Text,
                Approved = model.Approved,
                StudentName = model.User.FirstName + ' ' + model.User.LastName,
                CounselorName = model.Counselor.User.FirstName + ' ' + model.Counselor.User.LastName,
                CreateDate = $"{PersianDateConverter.ToPersianDate(model.CreateDate):yyyy/MM/dd}"
            };
    }
}