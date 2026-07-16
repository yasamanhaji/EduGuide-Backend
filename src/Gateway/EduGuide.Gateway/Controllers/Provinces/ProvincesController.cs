using Base.Api.Base;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.CQRS.Provinces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduGuide.Gateway.Controllers.Provinces
{
    public class ProvincesController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<Result<List<string>>> Dropdown([FromQuery] ProvincesDropdownQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
