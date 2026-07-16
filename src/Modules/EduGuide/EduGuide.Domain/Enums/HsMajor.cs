using System.ComponentModel;

namespace EduGuide.Domain.Enums
{
    public enum HsMajor
    {
        None = 0,

        [Description("ریاضی")]
        Math = 1,

        [Description("تجربی")]
        EmpiricalScience = 2,

        [Description("انسانی")]
        Humanity = 3,
    }
}
