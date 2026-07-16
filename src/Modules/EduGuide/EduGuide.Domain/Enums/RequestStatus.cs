using System.ComponentModel;

namespace EduGuide.Domain.Enums
{
    public enum RequestStatus
    {
        [Description("درخواست ثبت شده")]
        Requested = 1,

        [Description("تایید شده توسط ادمین")]
        ApprovedByAdmin = 2,

        [Description("تایید شده توسط مشاور، درانتظار تسویه")]
        ApprovedByCounselor = 3,

        [Description("تسویه مالی")]
        Settlement = 4,

        [Description("مشاوره به پایان رسیده")]
        Ended = 5,

        [Description("درخواست رد شده")]
        Rejected = 6,

        [Description("کنسل شده")]
        Canceled = 7
    }
}
