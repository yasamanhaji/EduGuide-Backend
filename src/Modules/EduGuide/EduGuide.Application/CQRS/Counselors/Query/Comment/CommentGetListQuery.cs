using Base.Application;
using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using MediatR;
using System.Linq.Dynamic.Core;

namespace EduGuide.Application.CQRS.Counselors
{ 
    public class CommentGetListQuery : IRequest<Result>
    {
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
    }

    public class CommentGetListQueryHandler(IGenericRepository<Comment, IEduGuideContext> genericRepository)
        : IRequestHandler<CommentGetListQuery, Result>
    {
        public async Task<Result> Handle(CommentGetListQuery request, CancellationToken cancellationToken)
        {
            var sort = "id desc";

            var model = new ItemListDTO<CommentGetListDTO>();
            model.PageSize = request.PageSize;
            model.TotalCount = await genericRepository.Repository.CountAsync();
            model.PageIndex = request.PageIndex;
            model.FilteredCount = await genericRepository.Repository.CountAsync();
            model.Items = await genericRepository.Repository.GetDTOAsync(
                CommentGetListDTO.Selector,
                orderBy: x => x.OrderBy(sort),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
                );

            return Result.Success(model);
        }
    }

}