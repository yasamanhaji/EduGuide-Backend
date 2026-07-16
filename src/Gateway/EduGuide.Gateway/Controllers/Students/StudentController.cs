using Base.Api.Base;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.CQRS.Students;
using EduGuide.Application.CQRS.Students.Command.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduGuide.Gateway.Controllers.Students
{
    public class StudentController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Result<StudentsProfileDto>>> Profile([FromQuery] StudentsProfileQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Result<bool>>> UpdateProfile([FromForm] StudentsUpdateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<Result<StudentGetByIdDTO>>> GetById([FromQuery] StudentGetByIdQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}