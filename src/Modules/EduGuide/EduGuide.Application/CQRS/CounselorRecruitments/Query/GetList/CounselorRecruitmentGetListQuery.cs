using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.CounselorRecruitments
{
    public class CounselorRecruitmentGetListQuery : IRequest<Result<List<CounselorRecruitmentGetListDTO>>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UniName { get; set; }
        public HsMajor? HsMajor { get; set; }
        public string Province { get; set; }
        public string UniMajor { get; set; } 

        public Expression<Func<CounselorRecruitment, bool>> GetFilter()
        {
            var filter = PredicateBuilder.New<CounselorRecruitment>(true);

            if (!string.IsNullOrEmpty(FirstName))
                filter.And(x => x.FirstName.Contains(FirstName));

            if (!string.IsNullOrEmpty(LastName))
                filter.And(x => x.LastName.Contains(LastName));

            if (!string.IsNullOrEmpty(Email))
                filter.And(x => x.Email.Contains(Email));

            if (!string.IsNullOrEmpty(UniName))
                filter.And(x => x.UniName.Contains(UniName));

            if (!string.IsNullOrEmpty(UniMajor))
                filter.And(x => x.UniMajor.Contains(UniMajor));

            if (HsMajor != null)
                filter.And(x => x.HsMajor == HsMajor);

            if (!string.IsNullOrEmpty(Province))
                filter.And(x => x.Province.Contains(Province));

            return filter;
        }
    }

    public class CounselorRecruitmentQueryHandler(IGenericRepository<CounselorRecruitment, IEduGuideContext> genericRepository, IMinIoService minIoService)
        : IRequestHandler<CounselorRecruitmentGetListQuery, Result<List<CounselorRecruitmentGetListDTO>>>
    {
        public async Task<Result<List<CounselorRecruitmentGetListDTO>>> Handle(CounselorRecruitmentGetListQuery request, CancellationToken cancellationToken)
        {
            var filter = request.GetFilter();

            var res = await genericRepository.Repository.GetDTOAsync(
                CounselorRecruitmentGetListDTO.Selector,
                filter
                );

            foreach (var counselorRecruitment in res)
                counselorRecruitment.FileUrl = await minIoService.GetDownloadUrl(counselorRecruitment.StudentCardPicName, "CounselorRecruitment/");

            return Result<List<CounselorRecruitmentGetListDTO>>.Success(res);
        }
    }
}