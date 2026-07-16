using Base.Api.Base;
using Base.Application;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.CQRS.Counselors;
using EduGuide.Application.CQRS.Counselors.Command.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduGuide.Gateway.Controllers.Counselors
{
    public class CounselorController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Counselor")]
        public async Task<ActionResult<Result<CounselorsProfileDto>>> Profile([FromQuery] CounselorsProfileQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "Counselor")]
        public async Task<ActionResult<Result<bool>>> Update([FromForm] CounselorsUpdateCommand query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Result<ItemListDTO<OurCounselorGetListDTO>>>> GetList([FromQuery] CounselorGetListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Result<CounselorGetByIdDTO>>> GetById([FromQuery] CounselorGetByIdQuery query)
        {
            return await Mediator.Send(query);
        }


        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Result>> Comment([FromBody] CounselorCommentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Result>> Comment([FromQuery] CommentGetListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPatch]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Result>> ApproveComment([FromForm] CommentAprroveCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Result>> CounselorComments([FromQuery] CounselorCommentGetListQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}