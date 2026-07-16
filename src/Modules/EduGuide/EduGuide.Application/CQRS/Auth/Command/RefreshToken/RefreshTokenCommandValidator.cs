using FluentValidation;

namespace EduGuide.Application.CQRS.Auth
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            //RuleFor(x => x.UserId)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("شناسه کاربری الزامی است!");

            RuleFor(x => x.RefreshToken)
                .NotNull()
                .NotEmpty()
                .WithMessage("رفرش توکن الزامی است!");
        }
    }
}