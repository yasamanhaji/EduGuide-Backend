using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EduGuide.Application.CQRS
{
    public class GetQuery : IRequest<Result<string>>
    {
        public IFormFile file { get; set; }
        //public string fileName { get; set; }
    }

    public class GetQueryHandler(IEduGuideUnitOfWork uow, IMinIoService minIoService, IJwtManager jwtManager) : IRequestHandler<GetQuery, Result<string>>
    {

        public async Task<Result<string>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            var userName = jwtManager.GetUserName();
            await minIoService.UploadFile(request.file, $"CounselorRecruitment/{userName}");
            var url = await minIoService.GetDownloadUrl(request.file.FileName, $"CounselorRecruitment/{userName}");
            return Result<string>.Success(url);
        }
    }
}
