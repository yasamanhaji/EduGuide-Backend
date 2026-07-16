using FluentValidation;

namespace EduGuide.Application.CQRS.Payments.Query.GetList
{
    public class PaymentGetListQueryValidator : AbstractValidator<PaymentGetListQuery>
    {
        public PaymentGetListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("سایز صفحه بزرگتر از صفر باید باشد!");

            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("شماره صفحه باید بزرگتر از صفر باشد!");
        }
    }
}