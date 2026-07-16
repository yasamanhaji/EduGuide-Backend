using FluentValidation;

namespace EduGuide.Application.CQRS.Counselors
{
    public class CounselorCommentCommandValidator : AbstractValidator<CounselorCommentCommand>
    {
        public CounselorCommentCommandValidator()
        {
            RuleFor(x => x.CounselorId)
                .NotEmpty().WithMessage("آیدی مشاور اجباری است!");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("متن کامنت اجباری است!")
                .MaximumLength(500).WithMessage("متن کامنت بیشتر از 500 کاراکتر نمی‌تواند باشد!");

        }
    }
}