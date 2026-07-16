using Base.Application;
using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using LinqKit;
using MediatR;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.RequestCounselors.Query.StudentCounselors
{
    public class StudentCounselorsGetListQuery
        : IRequest<Result>
    {
        public RequestStatus? Status { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;

        public Expression<Func<RequestCounselor, bool>> GetFilter()
        {
            var filter = PredicateBuilder.New<RequestCounselor>(true);

            if (Status != null)
                filter.And(x => x.status == Status);

            return filter;
        }
    }

    public class StudentCounselorsGetListQueryHandler(IJwtManager jwtManager, IEduGuideUnitOfWork uow, IMinIoService minIoService)
        : IRequestHandler<StudentCounselorsGetListQuery, Result>
    {
        public async Task<Result> Handle(StudentCounselorsGetListQuery request, CancellationToken cancellationToken)
        {
            var sort = "id desc";
            var userId = jwtManager.GetUserId();
            var role = jwtManager.GetRole();
            var filter = request.GetFilter();

            if (role == "Counselor")
                filter = filter.And(x => x.Counselor.UserId == userId);
            else if (role == "Student")
                filter = filter.And(x => x.Student.UserId == userId);

            var model = new ItemListDTO<MyCounselorsListDTO>();
            model.PageSize = request.PageSize;
            model.TotalCount = await uow.RequestCounselors.CountAsync();
            model.PageIndex = request.PageIndex;
            model.FilteredCount = await uow.RequestCounselors.CountAsync(filter);
            model.Items = await uow.RequestCounselors.GetDTOAsync(
                MyCounselorsListDTO.Selector,
                filter,
                orderBy: x => x.OrderBy(sort),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
                );
            model.Items.ForEach(async x => x.PicUrl = await minIoService.GetDownloadUrl(x.PicName, $"Counselor/Profile/{x.CounselorName}/"));
            foreach (var item in model.Items)
            {
                var rate = await uow.Rates.LastOrDefaultAsync(x => x.RequestCounselorId == item.Id, orderBy: x => x.OrderBy("id asc"));
                if (rate != null)
                {
                    item.Rate = rate.Score;
                    bool isRate = false;

                    if (rate.CreateDate.Date >= item.EndDate.Value.AddDays(-5).Date && rate.CreateDate <= item.EndDate.Value.AddDays(5).Date)
                    {
                        isRate = true;
                    }
                    if (!isRate && DateTime.Now.Date >= item.EndDate.Value.AddDays(-5).Date && DateTime.Now.Date <= item.EndDate.Value.AddDays(5).Date)
                        item.CanRate = true;
                }
                else if (item.RequestStatus == RequestStatus.Settlement)
                    item.CanRate = true;
            }

            return Result.Success(model);
        }
    }
}