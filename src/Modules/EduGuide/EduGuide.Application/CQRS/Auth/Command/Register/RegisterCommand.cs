using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EduGuide.Application.CQRS.Auth
{
    public class RegisterCommand : IRequest<Result<bool>>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }

    public class RegisterCommandHandler(IEduGuideUnitOfWork uow)
        : IRequestHandler<RegisterCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = "Student",
                UserName = request.FirstName + ' ' + request.LastName
            };
            user.PasswordHash = new PasswordHasher<User>()
                .HashPassword(user, request.Password);
            var student = new Student()
            {
                User = user
            };

            //await uow.Users.AddAsync(user);
            await uow.Students.AddAsync(student);


            await uow.CommitAsync();

            return Result<bool>.Success(true);
        }
    }
}