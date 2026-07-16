using System.Linq.Expressions;
using Base.Utilities.Extensions;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using FarsiLibrary.Utils;

namespace EduGuide.Application.CQRS.RequestCounselors.Query.GetList
{
    public class RequestCounselorGetListDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MajorTitle { get; set; }
        public string GradeLevel { get; set; }
        public double LastGradeGPA { get; set; }
        public string SchoolName { get; set; }
        public string AboutMe { get; set; }
        public string Province { get; set; }
        public string StartDate { get; set; }
        public int? RemainingDays { get; set; }
        public long StudentId { get; set; }
        public long? CounselorId { get; set; }
        public string CounselorName { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string StatusTitle { get; set; }
        public string CreateDate { get; set; }
        public double Rate { get; set; }
        public string PicName { get; set; }
        public string PicUrl { get; set; }

        public static Expression<Func<RequestCounselor, RequestCounselorGetListDTO>> Selector
        => model => new RequestCounselorGetListDTO()
        {
            Id = model.Id,
            FirstName = model.Student.User.FirstName,
            LastName = model.Student.User.LastName,
            MajorTitle = model.Student.Major.GetDescription(),
            GradeLevel = model.Student.GradeLevel.GetDescription(),
            LastGradeGPA = model.Student.LastGradeGPA,
            SchoolName = model.Student.SchoolName,
            AboutMe = model.Student.AboutMe,
            PicName = model.Student.PicName,
            Province = model.Student.Province,
            RemainingDays = (model.EndDate != null && model.status == RequestStatus.Settlement) ? (model.EndDate.Value.Date - DateTime.Now.Date).Days : null,
            CounselorId = model.CounselorId,
            CounselorName = model.Counselor.User.FirstName + ' ' + model.Counselor.User.LastName,
            RequestStatus = model.status,
            StatusTitle = model.status.GetDescription(),
            CreateDate = PersianDateConverter.ToPersianDate(model.CreateDate).ToString("yyyy/mm/dd"),
            StartDate = model.StartDate != null ? PersianDateConverter.ToPersianDate(model.StartDate.Value).ToString("yyyy/mm/dd") : null,
            StudentId = model.StudentId,
        };
    }
}