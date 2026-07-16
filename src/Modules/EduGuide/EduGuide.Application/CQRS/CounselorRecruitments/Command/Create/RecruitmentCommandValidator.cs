using EduGuide.Application.Contracts.Repositories;
using FluentValidation;

namespace EduGuide.Application.CQRS.CounselorRecruitments
{
    public class RecruitmentCommandValidator: AbstractValidator<RecruitmentCommand>
    {
        public RecruitmentCommandValidator(IEduGuideUnitOfWork uow)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام اجباری است!")
                .NotNull().WithMessage("نام نمی‌تواند مقدار null داشته باشد!")
                .MaximumLength(30).WithMessage("نام نمی‌تواند بیشتر از 30 کاراکتر باشد!");

            RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage("نام خانوادگی اجباری است!")
                    .NotNull().WithMessage("نام خانوادگی نمی‌تواند مقدار null داشته باشد!")
                    .MaximumLength(30).WithMessage("نام خانوادگی نمی‌تواند بیشتر از 30 کاراکتر باشد!");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("شماره تماس اجباری است!")
                .NotNull().WithMessage("شماره تماس نمی‌تواند مقدار null داشته باشد!")
                .Matches(@"^09\d{9}$").WithMessage("شماره تماس باید با 09 شروع شود و شامل 11 رقم باشد!");

            RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("ایمیل اجباری است!")
                    .NotNull().WithMessage("ایمیل نمی‌تواند مقدار null داشته باشد!")
                    .EmailAddress().WithMessage("فرمت ایمیل معتبر نیست!")
                    .MaximumLength(60).WithMessage("ایمیل نمی‌تواند بیشتر از 60 کاراکتر باشد!")
                    .Must(email => !uow.Users.Any(y => y.Email == email)).WithMessage("ایمیل تکراری است!");

            RuleFor(x => x.Province)
                .NotEmpty().WithMessage("استان اجباری است!")
                .NotNull().WithMessage("استان نمی‌تواند مقدار null داشته باشد!")
                .MaximumLength(50).WithMessage("نام استان نمی‌تواند بیشتر از 50 کاراکتر باشد!");

            RuleFor(x => x.HsMajor)
                .Must(value => value != 0).WithMessage("رشته تحصیلی اجباری است!")
                .NotEmpty().WithMessage("رشته تحصیلی اجباری است!")
                .NotNull().WithMessage("رشته تحصیلی نمی‌تواند مقدار null داشته باشد!");

            RuleFor(x => x.UniName)
                .NotEmpty().WithMessage("نام دانشگاه اجباری است!")
                .NotNull().WithMessage("نام دانشگاه نمی‌تواند مقدار null داشته باشد!")
                .MaximumLength(100).WithMessage("نام دانشگاه نمی‌تواند بیشتر از 100 کاراکتر باشد!");

            RuleFor(x => x.UniMajor)
               .NotEmpty().WithMessage("رشته دانشگاهی اجباری است!")
               .NotNull().WithMessage("سال ورود به دانشگاه نمی‌تواند مقدار null داشته باشد!");


            RuleFor(x => x.EntranceExamYear)
                    .NotEmpty().WithMessage("سال کنکور اجباری است!")
                    .NotNull().WithMessage("سال کنکور نمی‌تواند مقدار null داشته باشد!")
                    .Matches(@"^\d{4}$").WithMessage("سال کنکور باید چهار رقم باشد!");

            RuleFor(x => x.CountryRanking)
                    .NotEmpty().WithMessage("رتبه کشوری اجباری است!")
                    .NotNull().WithMessage("رتبه کشوری نمی‌تواند مقدار null داشته باشد!")
                    .Matches(@"^\d+$").WithMessage("رتبه کشوری باید فقط شامل اعداد باشد!")
                    .MaximumLength(10).WithMessage("رتبه کشوری نمی‌تواند بیشتر از 10 رقم باشد!");


            RuleFor(x => x.StudentCardPic)
                .NotEmpty().WithMessage("فایل اجباری است!")
                .NotNull().WithMessage("فایل اجباری است!");

            RuleFor(x => x.Employmenthistory)
                    .NotEmpty().WithMessage("سوابق شغلی اجباری است!")
                    .NotNull().WithMessage("سوابق شغلی نمی‌تواند مقدار null داشته باشد!")
                    .MaximumLength(500).WithMessage("سوابق شغلی نمی‌تواند بیشتر از 500 کاراکتر باشد!");
        }
    }
}