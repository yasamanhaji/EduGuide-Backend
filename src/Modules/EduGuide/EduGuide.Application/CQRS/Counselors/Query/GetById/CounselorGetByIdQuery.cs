using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using MediatR;

namespace EduGuide.Application.CQRS.Counselors
{
    public class CounselorGetByIdQuery : IRequest<Result<CounselorGetByIdDTO>>
    {
        public long Id { get; set; }
    }

    public class CounselorGetByIdQueryHandler(IEduGuideUnitOfWork uow, IMinIoService minIoService, IJwtManager jwtManager)
        : IRequestHandler<CounselorGetByIdQuery, Result<CounselorGetByIdDTO>>
    {
        public async Task<Result<CounselorGetByIdDTO>> Handle(CounselorGetByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await uow.Counselors.GetOneDTOAsync(
                CounselorGetByIdDTO.Selector,
                x => x.Id == request.Id);
            model.PicUrl = await minIoService.GetDownloadUrl(model.PicName, $"Counselor/Profile/{model.FullName}");

            if (jwtManager.GetUserId() != null && jwtManager.GetRole() == "Student")
            {
                var userId = jwtManager.GetUserId();
                var reqCounselor = await uow.RequestCounselors.FirstOrDefaultAsync(x => x.Student.UserId == userId && (x.status == Domain.Enums.RequestStatus.ApprovedByCounselor
                || x.status == Domain.Enums.RequestStatus.Settlement || x.status == Domain.Enums.RequestStatus.Requested));

                if (reqCounselor != null)
                {
                    model.StudentCounselorId = reqCounselor.CounselorId;
                    model.RequestStatus = reqCounselor.status;
                }
            }

            return Result.Success(model);
        }
    }
}