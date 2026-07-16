using Base.Application;
using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using LinqKit;
using MediatR;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;


namespace EduGuide.Application.CQRS.Counselors
{
    public class CounselorGetListQuery : IRequest<Result<ItemListDTO<OurCounselorGetListDTO>>>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        //public string UniName { get; set; }
        public string FullName { get; set; }
        public HsMajor? HsMajor { get; set; }

        public Expression<Func<Counselor, bool>> GetFilter()
        {
            var filter = PredicateBuilder.New<Counselor>(true);

            //if (UniName != null)
            //    filter.And(x => x.CounselorRecruitment.UniName.Contains(UniName));

            if (FullName != null)
                filter.And(x => (x.CounselorRecruitment.FirstName + ' ' + x.CounselorRecruitment.LastName).Contains(FullName));

            if (HsMajor != null)
                filter.And(x => x.CounselorRecruitment.HsMajor == HsMajor.Value);

            return filter;
        }
    }

    public class CounselorGetListQueryHandler(IGenericRepository<Counselor, IEduGuideContext> genericRepository, IMinIoService minIoService)
        : IRequestHandler<CounselorGetListQuery, Result<ItemListDTO<OurCounselorGetListDTO>>>
    {
        public async Task<Result<ItemListDTO<OurCounselorGetListDTO>>> Handle(CounselorGetListQuery request, CancellationToken cancellationToken)
        {
            var sort = "id asc";
            var filter = request.GetFilter();

            var model = new ItemListDTO<OurCounselorGetListDTO>();
            model.PageSize = request.PageSize;
            model.TotalCount = await genericRepository.Repository.CountAsync();
            model.PageIndex = request.PageIndex;
            model.FilteredCount = await genericRepository.Repository.CountAsync(filter);
            model.Items = await genericRepository.Repository.GetDTOAsync(
                OurCounselorGetListDTO.Selector,
                filter,
                orderBy: x => x.OrderBy(sort),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
                );
            model.Items.ForEach(async x => x.PicUrl = await minIoService.GetDownloadUrl(x.PicName, $"Counselor/Profile/{x.FullName}"));

            return Result.Success(model);
        }
    }
}