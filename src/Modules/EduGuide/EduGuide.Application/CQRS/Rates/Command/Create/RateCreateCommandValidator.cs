using FluentValidation;

namespace EduGuide.Application.CQRS.Rates
{
    public class RateCreateCommandValidator : AbstractValidator<RateCreateCommand>
    {
        public RateCreateCommandValidator()
        {
            RuleFor(x => x.RequestCounselorId)
                .GreaterThan(0).WithMessage("آیدی اجباری است!");

            RuleFor(x => x.Score)
                .GreaterThan(0).WithMessage("امتیاز اجباری است!");
        }
    }
}