using Base.Application.Contracts;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using FluentValidation;

namespace EduGuide.Application.CQRS.Counselors.Command.Update
{
    public class CounselorsUpdateCommandValidator : AbstractValidator<CounselorsUpdateCommand>
    {
        public CounselorsUpdateCommandValidator(IGenericRepository<User, IEduGuideContext> genericRepository, IJwtManager jwtManager)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("ایمیل اجباری است!")
                .EmailAddress().WithMessage("فرمت ایمیل معتبر نیست!")
                .Must((request, email) =>
                {
                    var currentUserId = jwtManager.GetUserId();
                    return !genericRepository.Repository.Any(u => u.Id != currentUserId && u.Email == email);
                })
                .WithMessage("این ایمیل قبلاً ثبت شده است!")
                .MaximumLength(60).WithMessage("ایمیل نمی‌تواند بیشتر از 60 کاراکتر باشد!");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("شماره تلفن اجباری است!")
                .Matches(@"^09\d{9}$").WithMessage("شماره تماس باید با 09 شروع شود و شامل 11 رقم باشد!");

            RuleFor(x => x.Province)
                .NotEmpty().WithMessage("استان اجباری است!")
                .NotNull().WithMessage("استان نمی‌تواند مقدار null داشته باشد!")
                .MaximumLength(50).WithMessage("نام استان نمی‌تواند بیشتر از 50 کاراکتر باشد!");

            RuleFor(x => x.Employmenthistory)
                .NotEmpty().WithMessage("سابقه کار اجباری است!")
                .MaximumLength(500).WithMessage("سوابق شغلی نمی‌تواند بیشتر از 500 کاراکتر باشد!");

            RuleFor(x => x.AboutMe)
                .NotEmpty().WithMessage("درباره من اجباری است!")
                .MaximumLength(2500).WithMessage("درباره من نمی‌تواند بیشتر از 2500 کاراکتر باشد!");

        }
    }
}