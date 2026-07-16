using Base.Api.Base;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.CQRS.Rates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduGuide.Gateway.Controllers.Rates
{
    public class RatesController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Result>> Create([FromBody] RateCreateCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}