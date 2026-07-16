using Base.Utilities.Extensions;
using EduGuide.Domain.Entities;
using FarsiLibrary.Utils;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Students
{
    public class StudentsProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MajorTitle { get; set; }
        public string GradeLevel { get; set; }
        public double LastGradeGPA { get; set; }
        public string AboutMe { get; set; }
        public string StudentPhoneNumber { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string BirthDate { get; set; }
        public string SchoolName { get; set; }
        public string Province { get; set; }
        public string ProfilePicName { get; set; }
        public string ProfilePicUrl { get; set; }

        public static Expression<Func<Student, StudentsProfileDto>> Selector
            => model => new StudentsProfileDto()
            {
                Id = model.Id,
                FirstName = model.User.FirstName,
                LastName = model.User.LastName,
                Email = model.User.Email,
                MajorTitle = model.Major.GetDescription(),
                GradeLevel = model.GradeLevel.GetDescription(),
                LastGradeGPA = model.LastGradeGPA,
                StudentPhoneNumber = model.StudentPhoneNumber,
                BirthDate = model.BirthDate,
                ParentPhoneNumber = model.ParentPhoneNumber,
                SchoolName = model.SchoolName,
                AboutMe = model.AboutMe,
                ProfilePicName = model.PicName,
                Province = model.Province,
            };
    }
}