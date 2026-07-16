using Base.Application;
using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace EduGuide.Application.CQRS.Payments
{
    public class CounselorPaymentsQuery
        : IRequest<Result<ItemListDTO<CounselorPaymentsDTO>>>
    {
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;

        [BindNever]
        public long UserId { get; set; }

        public Expression<Func<Payment, bool>> GetFilter()
        {
            var filter = PredicateBuilder.New<Payment>(true);

            filter.And(x => x.Counselor.UserId == UserId);

            return filter;
        }
    }

    public class CounselorPaymentsQueryHandler(IGenericRepository<Payment, IEduGuideContext> genericRepository, IJwtManager jwtManager)
        : IRequestHandler<CounselorPaymentsQuery, Result<ItemListDTO<CounselorPaymentsDTO>>>
    {
        public async Task<Result<ItemListDTO<CounselorPaymentsDTO>>> Handle(CounselorPaymentsQuery request, CancellationToken cancellationToken)
        {
            var sort = "id desc";
            request.UserId = jwtManager.GetUserId().Value;
            var filter = request.GetFilter();

            var model = new ItemListDTO<CounselorPaymentsDTO>();
            model.PageSize = request.PageSize;
            model.TotalCount = await genericRepository.Repository.CountAsync();
            model.PageIndex = request.PageIndex;
            model.FilteredCount = await genericRepository.Repository.CountAsync(filter);
            model.Items = await genericRepository.Repository.GetDTOAsync(
                CounselorPaymentsDTO.Selector,
                filter,
                orderBy: x => x.OrderBy(sort),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
                );

            return Result.Success(model);
        }
    }
}
