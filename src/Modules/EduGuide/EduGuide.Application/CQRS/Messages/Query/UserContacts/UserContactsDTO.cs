using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Messages
{
    public class UserContactsDTO
    {
        public long ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactProfilePicUrl { get; set; }
        public string PicName { get; set; }
        public string Role { get; set; }
        public string LastMessage { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public static Expression<Func<RequestCounselor, UserContactsDTO>> StudentSelector =>
            model => new UserContactsDTO
            {
                ContactId = model.Counselor.UserId,
                ContactName = model.Counselor.User.UserName,
                Role = model.Counselor.User.Role,
                PicName = model.Counselor.ProfilePicName,
                RequestStatus = model.status
            };
        public static Expression<Func<RequestCounselor, UserContactsDTO>> CounselorSelector =>
            model => new UserContactsDTO
            {
                ContactId = model.Student.UserId,
                ContactName = model.Student.User.UserName,
                Role = model.Student.User.Role,
                PicName = model.Student.PicName,
                RequestStatus = model.status
            };
    }
}