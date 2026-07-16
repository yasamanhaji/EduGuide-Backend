using Base.Application.Contracts;
using Base.Application.Contracts.DTOs;
using Base.Application.Contracts.DTOs.Common;
using Base.Application.Exceptions;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EduGuide.Application.CQRS.Auth
{
    public class LoginCommand : IRequest<Result<UserDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler(IJwtManager jwtManager, IGenericRepository<User, IEduGuideContext> uow, IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<LoginCommand, Result<UserDTO>>
    {
        public async Task<Result<UserDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await uow.Repository.FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            if (user == null ||
                new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                throw new LoginException("ایمیل یا رمزعبور اشتباه است!");

            var userDto = new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role,
                Email = user.Email,
            };
            
            var accessToken = jwtManager.CreateToken(userDto);
            userDto.AccessToken = accessToken;
            userDto.AccessTokenExpirtTime = DateTime.Now.AddDays(1);

            var refreshToken = jwtManager.GenerateRefreshToken();
            var refreshTokenExpiryTime = DateTime.Now.AddDays(7);

            userDto.RefreshToken = refreshToken;
            userDto.RefreshTokenExpiryTime = refreshTokenExpiryTime;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;

            await uow.CommitAsync();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(1)
            };
            httpContextAccessor.HttpContext?.Response.Cookies.Append("access_token", accessToken, cookieOptions);

            return Result<UserDTO>.Success(userDto);
        }
    }
}