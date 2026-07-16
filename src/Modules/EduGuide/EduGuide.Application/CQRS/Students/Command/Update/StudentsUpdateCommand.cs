using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EduGuide.Application.CQRS.Students.Command.Update
{
    public class StudentsUpdateCommand : IRequest<Result<bool>>
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public GradeLevel GradeLevel { get; set; }
        public HsMajor Major { get; set; }
        public double LastGradeGPA { get; set; }
        public string AboutMe { get; set; }
        public string StudentPhoneNumber { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string SchoolName { get; set; }
        public string Province { get; set; }
        public string BirthDate { get; set; }
        public IFormFile ProfilePic { get; set; }
    }
    public class StudentsUpdateCommandHandler(IGenericRepository<Student, IEduGuideContext> genericRepository, IMinIoService minIoService,
        IJwtManager jwtManager)
       : IRequestHandler<StudentsUpdateCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(StudentsUpdateCommand request, CancellationToken cancellationToken)
        {
            var student = await genericRepository.Repository
                .FirstOrDefaultAsync(
                c => c.Id == request.Id,
                query => query
                .Include(c => c.User)
                );

            if (student == null)
                throw new Exception("رکورد موردنظر یافت نشد!");

            student.User.Email = request.Email;
            student.StudentPhoneNumber = request.StudentPhoneNumber;
            student.GradeLevel = request.GradeLevel;
            student.SchoolName = request.SchoolName;
            student.ParentPhoneNumber = request.ParentPhoneNumber;
            student.AboutMe = request.AboutMe;
            student.LastGradeGPA = request.LastGradeGPA;
            student.Major = request.Major;
            student.Province = request.Province;
            student.BirthDate = request.BirthDate;
            student.IsProfileComplete = true;

            if (request.ProfilePic != null)
            {
                await minIoService.UploadFile(request.ProfilePic, $"Student/Profile/{jwtManager.GetUserName()}/");
                student.PicName = request.ProfilePic.FileName;
            }

            await genericRepository.CommitAsync();
            return Result<bool>.Success(true);
        }
    }
}