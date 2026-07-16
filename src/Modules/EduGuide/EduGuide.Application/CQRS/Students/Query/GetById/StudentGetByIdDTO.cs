using Base.Utilities.Extensions;
using EduGuide.Domain.Entities;
using FarsiLibrary.Utils;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Students
{
    public class StudentGetByIdDTO
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string GradeLevel { get; set; }
        public double LastGradeGPA { get; set; }
        public string Major { get; set; }
        public string Province { get; set; }
        public string AboutMe { get; set; }
        public string StudentPhoneNumber { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string BirthDate { get; set; }
        public string SchoolName { get; set; }
        public string PicName { get; set; }
        public string PicUrl { get; set; }

        public static Expression<Func<Student, StudentGetByIdDTO>> Selector =>
            model => new StudentGetByIdDTO
            {
                Id = model.Id,
                FullName = model.User.FirstName + ' ' + model.User.LastName,
                GradeLevel = model.GradeLevel.GetDescription(),
                LastGradeGPA = model.LastGradeGPA,
                Major = model.Major.GetDescription(),
                Province = model.Province,
                AboutMe = model.AboutMe,
                StudentPhoneNumber = model.StudentPhoneNumber,
                ParentPhoneNumber = model.ParentPhoneNumber,
                BirthDate = model.BirthDate,
                SchoolName = model.SchoolName,
                PicName = model.PicName,
            };
    }
}