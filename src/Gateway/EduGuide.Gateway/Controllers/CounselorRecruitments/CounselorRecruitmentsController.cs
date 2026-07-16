using Base.Api.Base;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.CQRS.CounselorRecruitments;
using EduGuide.Application.CQRS.CounselorRecruitments.Command.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduGuide.Gateway.Controllers.CounselorRecruitment
{
    public class CounselorRecruitmentsController(IMediator mediator): BaseApiController(mediator)
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Result<bool>>> Create([FromForm] RecruitmentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Result<List<CounselorRecruitmentGetListDTO>>>> GetList([FromQuery] CounselorRecruitmentGetListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Result<bool>>> Update([FromForm] CounselorRecruitmentUpdateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Result<bool>>> Approve([FromForm] CounselorApproveCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}