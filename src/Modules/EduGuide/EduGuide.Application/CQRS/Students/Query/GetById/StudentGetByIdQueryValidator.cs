using FluentValidation;

namespace EduGuide.Application.CQRS.Students
{
    public class StudentGetByIdQueryValidator
        : AbstractValidator<StudentGetByIdQuery>
    {
        public StudentGetByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("آیدی بزرگتر از صفر است!");
        }
    }
}
