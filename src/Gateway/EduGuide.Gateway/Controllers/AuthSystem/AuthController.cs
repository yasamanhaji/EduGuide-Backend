using Base.Api.Base;
using Base.Application.Contracts.DTOs;
using Base.Application.Contracts.DTOs.Common;
using Base.Infrastructure.Implementation.Filter;
using EduGuide.Application.CQRS.Auth;
using EduGuide.Application.CQRS.Auth.Query.VerifyCode;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduGuide.Gateway.Controllers.AuthSystem
{
    public class AuthController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpPost]
        [PreventLoggedInAccess]
        [Route("[action]")]
        public async Task<ActionResult<Result<bool>>> Register(RegisterCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [PreventLoggedInAccess]
        [Route("[action]")]
        public async Task<ActionResult<Result<UserDTO>>> Login(LoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Result<bool>>> Logout(LogoutCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Result<TokenResponseDto>>> RefreshToken(RefreshTokenCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Result<bool>>> ForgotPassword(ForgotPasswordCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Result<bool>>> ChangeForgotPassword(ChangeForgotPasswordCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Result<bool>>> VerifyCode(VerifyCodeQuery command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<Result>> ProfileChangePassword(ChangePasswordCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}
