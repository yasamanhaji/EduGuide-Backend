using Base.Application;
using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Application.CQRS.Counselors;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Payments
{
    public class PaymentGetListQuery
        : IRequest<Result<ItemListDTO<PaymentListDTO>>>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        [BindNever]
        public long UserId { get; set; }

        public Expression<Func<Payment, bool>> GetFilter()
        {
            var filter = PredicateBuilder.New<Payment>(true);
            return filter;
        }
    }

    public class PaymentGetListQueryHandler(IGenericRepository<Payment, IEduGuideContext> genericRepository, IJwtManager jwtManager)
        : IRequestHandler<PaymentGetListQuery, Result<ItemListDTO<PaymentListDTO>>>
    {
        public async Task<Result<ItemListDTO<PaymentListDTO>>> Handle(PaymentGetListQuery request, CancellationToken cancellationToken)
        {
            var sort = "id desc";
            var userId = jwtManager.GetUserId();
            request.UserId = userId.Value;
            var filter = request.GetFilter();

            if (jwtManager.GetRole() == "Counselor")
                filter = filter.And(x => x.Counselor.UserId == userId);
            else
                filter = filter.And(x => x.Student.UserId == userId);


            var model = new ItemListDTO<PaymentListDTO>();
            model.PageSize = request.PageSize;
            model.TotalCount = await genericRepository.Repository.CountAsync();
            model.PageIndex = request.PageIndex;
            model.FilteredCount = await genericRepository.Repository.CountAsync(filter);
            model.Items = await genericRepository.Repository.GetDTOAsync(
                PaymentListDTO.Selector,
                filter,
                orderBy: x => x.OrderBy(sort),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
                );

            return Result.Success(model);
        }
    }
}