using FluentValidation;

namespace EduGuide.Application.CQRS.Counselors
{
    public class CounselorGetByIdQueryValidator : AbstractValidator<CounselorGetByIdQuery>
    {
        public CounselorGetByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("آیدی مقدار بالاتر از صفر باید داشته باشد!");
        }
    }
}
