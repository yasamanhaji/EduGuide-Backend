using System.ComponentModel;

namespace EduGuide.Domain.Enums
{
    public enum GradeLevel
    {
        None = 0,

        [Description("دهم")]
        Tenth = 1,

        [Description("یازدهم")]
        Eleventh = 2,

        [Description("دوازدهم")]
        Twelth = 3,
    }
}