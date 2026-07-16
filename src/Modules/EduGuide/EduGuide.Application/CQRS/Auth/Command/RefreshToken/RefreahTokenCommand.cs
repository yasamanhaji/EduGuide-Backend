using Base.Application.Contracts;
using Base.Application.Contracts.DTOs;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using MediatR;

namespace EduGuide.Application.CQRS.Auth
{
    public class RefreshTokenCommand : IRequest<Result<TokenResponseDto>>
    {
        //public long UserId { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenCommandHandler(IJwtManager jwtManager, IGenericRepository<User, IEduGuideContext> uow)
        : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
    {
        public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userId = jwtManager.GetUserId();
            var user = await uow.Repository.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                throw new Exception("شناسه کاربری نامعتبر است!");

            var userDto = new UserDTO()
            {
                UserName = user.UserName,
                Id = user.Id,
                Role = user.Role,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime.Value
            };
            var tokenResponseDto = jwtManager.RefreshTokensAsync(userDto, request.RefreshToken);

            user.RefreshToken = tokenResponseDto.RefreshToken;
            await uow.CommitAsync();

            return Result<TokenResponseDto>.Success(tokenResponseDto);
        }
    }
}