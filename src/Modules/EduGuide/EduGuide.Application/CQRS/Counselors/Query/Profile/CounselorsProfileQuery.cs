using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using MediatR;

namespace EduGuide.Application.CQRS.Counselors
{
    public class CounselorsProfileQuery : IRequest<Result<CounselorsProfileDto>>
    {


    }

    public class CounselorsQueryHandler(IGenericRepository<Counselor, IEduGuideContext> genericRepository, IJwtManager jwtManager,
        IMinIoService minIoService)
       : IRequestHandler<CounselorsProfileQuery, Result<CounselorsProfileDto>>
    {
        public async Task<Result<CounselorsProfileDto>> Handle(CounselorsProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = jwtManager.GetUserId();

            var res = await genericRepository.Repository.GetDTOAsync
                (
                    CounselorsProfileDto.Selector,
                    x => x.UserId == userId
                );

            if (res[0].StudentCardPicName != null)
                res[0].StudenCardPiceUrl = await minIoService.GetDownloadUrl(res[0].StudentCardPicName, "CounselorRecruitment/");
            if(res[0].ProfilePicName != null)
                res[0].ProfilePicUrl = await minIoService.GetDownloadUrl(res[0].ProfilePicName, $"Counselor/Profile/{jwtManager.GetUserName()}");

            return Result<CounselorsProfileDto>.Success(res[0]);
        }
    }
}