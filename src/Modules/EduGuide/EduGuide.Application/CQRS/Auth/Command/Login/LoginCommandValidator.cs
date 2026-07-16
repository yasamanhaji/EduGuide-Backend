using FluentValidation;

namespace EduGuide.Application.CQRS.Auth
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("ایمیل اجباری است!")
                .EmailAddress()
                .WithMessage("فرمت ایمیل درست نیست!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("پسوورد اجباری است!");
        }
    }
}
