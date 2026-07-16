using FluentValidation;

namespace EduGuide.Application.CQRS.Messages.Command.Create
{
    public class MessageCreateCommandValidator : AbstractValidator<MessageCreateCommand>
    {
        public MessageCreateCommandValidator()
        {
            RuleFor(x => x.ReceiverId)
                .GreaterThan(0).WithMessage("آیدی گیرنده اجباری است!");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("متن پیام اجباری است!");
        }
    }
}