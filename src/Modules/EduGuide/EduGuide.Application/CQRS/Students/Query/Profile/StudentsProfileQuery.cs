using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using MediatR;

namespace EduGuide.Application.CQRS.Students
{
    public class StudentsProfileQuery : IRequest<Result<StudentsProfileDto>>
    {
    }
    public class StudentsQueryHandler(IGenericRepository<Student, IEduGuideContext> genericRepository, IJwtManager jwtManager,
        IMinIoService minIoService)
      : IRequestHandler<StudentsProfileQuery, Result<StudentsProfileDto>>
    {
        public async Task<Result<StudentsProfileDto>> Handle(StudentsProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = jwtManager.GetUserId();

            var res = await genericRepository.Repository.GetDTOAsync
                (
                    StudentsProfileDto.Selector,
                    x => x.UserId == userId

                );
            if (res[0].ProfilePicName != null)
                res[0].ProfilePicUrl = await minIoService.GetDownloadUrl(res[0].ProfilePicName, $"Student/Profile/{jwtManager.GetUserName()}/");

            /*                var originalUrl = await minIoService.GetDownloadUrl(res[0].ProfilePicName, $"Student/Profile/{jwtManager.GetUserName()}/");
                Uri uri = new Uri(originalUrl);
                string path = uri.AbsolutePath;
                string publicBaseUrl = "http://62.60.213.13:9000";
                string newUrl = $"{publicBaseUrl}{path}";
                res[0].ProfilePicUrl = newUrl;*/

            return Result<StudentsProfileDto>.Success(res[0]);
        }
    }
}