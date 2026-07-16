using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using MediatR;

namespace EduGuide.Application.CQRS.Students
{
    public class StudentGetByIdQuery
        : IRequest<Result<StudentGetByIdDTO>>
    {
        public long Id { get; set; }
    }

    public class StudentGetByIdQueryHandler(IEduGuideUnitOfWork uow, IMinIoService minIoService)
        : IRequestHandler<StudentGetByIdQuery, Result<StudentGetByIdDTO>>
    {
        public async Task<Result<StudentGetByIdDTO>> Handle(StudentGetByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await uow.Students.GetOneDTOAsync(
                StudentGetByIdDTO.Selector,
                x => x.Id == request.Id);
            if (student.PicName != null)
                student.PicUrl = await minIoService.GetDownloadUrl(student.PicName, $"Student/Profile/{student.FullName}/");

            return Result.Success(student);
        }
    }
}
