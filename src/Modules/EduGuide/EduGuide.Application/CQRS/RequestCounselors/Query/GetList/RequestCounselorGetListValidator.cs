using EduGuide.Application.CQRS.RequestCounselors.Query.GetList;
using FluentValidation;

namespace EduGuide.Application.CQRS.RequestCounselors
{
    public class RequestCounselorGetListQueryValidator : AbstractValidator<RequestCounselorGetListQuery>
    {
        public RequestCounselorGetListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("سایز صفحه بزرگتر از صفر باید باشد!");

            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("شماره صفحه باید بزرگتر از صفر باشد!");
        }
    }
}
