using Base.Utilities.Extensions;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using FarsiLibrary.Utils;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.RequestCounselors.Query.StudentCounselors
{
    public class MyCounselorsListDTO
    {
        public long Id { get; set; }
        public string CounselorName { get; set; }
        public long CounselorId { get; set; }
        public long StudentId { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string RequestStatusTitle => RequestStatus.GetDescription();
        public string StartDate { get; set; }
        public string PersianEndDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? RemainingDays { get; set; }
        public double? Rate { get; set; }
        public string PicName { get; set; }
        public string PicUrl { get; set; }
        public bool CanRate { get; set; }

        public static Expression<Func<RequestCounselor, MyCounselorsListDTO>> Selector
            => model => new MyCounselorsListDTO()
            {
                Id = model.Id,
                RemainingDays = (model.EndDate != null && model.status == RequestStatus.Settlement) ? (model.EndDate.Value.Date - DateTime.Now.Date).Days : null,
                CounselorId = model.CounselorId,
                CounselorName = model.Counselor.User.FirstName + ' ' + model.Counselor.User.LastName,
                RequestStatus = model.status,
                StartDate = model.StartDate != null ? PersianDateConverter.ToPersianDate(model.StartDate.Value).ToString("yyyy/mm/dd") : null,
                PersianEndDate = model.EndDate != null ? PersianDateConverter.ToPersianDate(model.EndDate.Value).ToString("yyyy/mm/dd") : null,
                PicName = model.Counselor.ProfilePicName,
                StudentId = model.StudentId,
                EndDate = model.EndDate != null ? model.EndDate : null,
            };
    }
}