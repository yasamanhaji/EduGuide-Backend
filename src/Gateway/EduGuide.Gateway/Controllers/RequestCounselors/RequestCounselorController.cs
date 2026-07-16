using Base.Api.Base;
using Base.Application;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.CQRS.RequestCounselors;
using EduGuide.Application.CQRS.RequestCounselors.Command.Approve;
using EduGuide.Application.CQRS.RequestCounselors.Command.Delete;
using EduGuide.Application.CQRS.RequestCounselors.Command.Create;
using EduGuide.Application.CQRS.RequestCounselors.Command.Delete;

using EduGuide.Application.CQRS.RequestCounselors.Command.Reject;
using EduGuide.Application.CQRS.RequestCounselors.Query.GetList;
using EduGuide.Application.CQRS.RequestCounselors.Query.StudentCounselors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EduGuide.Application.CQRS.RequestCounselors;
using EduGuide.Application.CQRS.RequestCounselors.Query.StudentCounselors;

namespace EduGuide.Gateway.Controllers.RequestCounselors
{
    public class RequestCounselorController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Student")]

        public async Task<ActionResult<Result<bool>>> Create([FromForm] RequestCreateCommand command)
        {
            return await Mediator.Send(command);
        }


        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Counselor, Student")]

        public async Task<ActionResult<Result<ItemListDTO<RequestCounselorGetListDTO>>>> GetList([FromQuery] RequestCounselorGetListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Counselor")]

        public async Task<ActionResult<Result<bool>>> Approve([FromForm] RequestApproveCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "Student")]

        public async Task<ActionResult<Result<bool>>> Delete([FromForm] RequestDeleteCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Counselor")]
        public async Task<ActionResult<Result>> Reject([FromBody] RequestCounselorRejectCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Result>> Cancel([FromBody] RequestCancelCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Result>> Extend([FromBody] RequestExtendCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Counselor")]
        public async Task<ActionResult<Result>> MyStudents([FromQuery] CounselorStudentsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Result>> MyCounselors([FromQuery] StudentCounselorsGetListQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}