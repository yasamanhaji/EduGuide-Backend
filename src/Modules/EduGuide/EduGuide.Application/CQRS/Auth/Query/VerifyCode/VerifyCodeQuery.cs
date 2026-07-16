using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using MediatR;

namespace EduGuide.Application.CQRS.Auth.Query.VerifyCode
{
    public class VerifyCodeQuery : IRequest<Result<bool>>
    {
        public string Email { get; set; }
        public int VerificationCode { get; set; }
    }

    public class VerifyCodeQueryHandler(IRedisService redisService)
        : IRequestHandler<VerifyCodeQuery, Result<bool>>
{
        public async Task<Result<bool>> Handle(VerifyCodeQuery request, CancellationToken cancellationToken)
        {
            var code = await redisService.GetVerificationCodeAsync(request.Email);
            if (string.IsNullOrEmpty(code) || long.Parse(code) != request.VerificationCode)
                throw new InvalidOperationException("کد منقضی یا نامتعبر است!");

            await redisService.SetVerificationCodeAsync(request.Email, "1", TimeSpan.FromMinutes(5));

            return Result<bool>.Success(true);
        }
    }
}
