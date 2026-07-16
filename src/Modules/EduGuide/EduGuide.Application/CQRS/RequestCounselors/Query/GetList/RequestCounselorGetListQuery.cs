using Base.Application;
using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using LinqKit;
using MediatR;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace EduGuide.Application.CQRS.RequestCounselors.Query.GetList
{
    public class RequestCounselorGetListQuery : IRequest<Result<ItemListDTO<RequestCounselorGetListDTO>>>
    {
        public HsMajor? Major {  get; set; }
        public GradeLevel? GradeLevel { get; set; }
        public RequestStatus? Status { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public Expression<Func<RequestCounselor, bool>> GetFilter()
        {
            var filter = PredicateBuilder.New<RequestCounselor>(true);

            if (Major != null)
                filter.And(x => x.Student.Major == Major);

            if (GradeLevel != null)
                filter.And(x => x.Student.GradeLevel == GradeLevel);

            if (Status != null)
                filter.And(x => x.status == Status);
            else
                filter.And(x => x.status == RequestStatus.Requested || x.status == RequestStatus.ApprovedByCounselor || x.status == RequestStatus.Canceled
                || x.status == RequestStatus.Rejected);

            return filter;
        }
    }

    public class RequestCounselorGetListQueryHandler(IEduGuideUnitOfWork uow,IJwtManager jwtManager, IMinIoService minIoService)
        : IRequestHandler<RequestCounselorGetListQuery, Result<ItemListDTO<RequestCounselorGetListDTO>>>
    {
        public async Task<Result<ItemListDTO<RequestCounselorGetListDTO>>> Handle(RequestCounselorGetListQuery request, CancellationToken cancellationToken)
        {
            var sort = "id desc";
            var userId = jwtManager.GetUserId();
            var role = jwtManager.GetRole();
            var filter = request.GetFilter();

            if (role == "Counselor")
                filter = filter.And(x => x.Counselor.UserId == userId);
            else if (role == "Student")
                filter = filter.And(x => x.Student.UserId == userId);

            var model = new ItemListDTO<RequestCounselorGetListDTO>();
            model.PageSize = request.PageSize;
            model.TotalCount = await uow.RequestCounselors.CountAsync();
            model.PageIndex = request.PageIndex;
            model.FilteredCount = await uow.RequestCounselors.CountAsync(filter);
            model.Items = await uow.RequestCounselors.GetDTOAsync(
                RequestCounselorGetListDTO.Selector,
                filter,
                orderBy: x => x.OrderBy(sort),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
                );
            model.Items.ForEach(async x => x.PicUrl = await minIoService.GetDownloadUrl(x.PicName, $"Student/Profile/{x.FirstName + ' ' + x.LastName}/"));

            return Result<ItemListDTO<RequestCounselorGetListDTO>>.Success(model);
        }
    }
}