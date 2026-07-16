using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Application.Contracts.DTOs.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EduGuide.Application.CQRS.Auth
{
    public class LogoutCommand: IRequest<Result<bool>>
    {
     
    }
    public class LogoutCommandHandler(IHttpContextAccessor httpContextAccessor) : IRequestHandler<LogoutCommand, Result<bool>>
    {
        public Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            httpContextAccessor.HttpContext?.Response.Cookies.Delete("access_token");

            return Task.FromResult(Result<bool>.Success(true));
        }
    }
}
