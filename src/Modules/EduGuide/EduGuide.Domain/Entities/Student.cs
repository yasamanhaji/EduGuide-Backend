using Base.Domain.Entities.Common;
using EduGuide.Domain.Enums;

namespace EduGuide.Domain.Entities
{
    public class Student : BaseEntity
    {
        public GradeLevel GradeLevel { get; set; }
        public double LastGradeGPA { get; set; }
        public HsMajor Major { get; set; }
        public string Province { get; set; }
        public bool IsProfileComplete { get; set; } = false;
        public string AboutMe { get; set; }
        public string StudentPhoneNumber { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string BirthDate { get; set; }
        public string SchoolName { get; set; }
        public string PicName { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}