using Base.Api.Base;
using Base.Application;
using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.CQRS.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduGuide.Gateway.Controllers.Messages
{
    public class MessagesController(IMediator mediator, IMinIoService minIoService) : BaseApiController(mediator)
    {
        [HttpGet]
        [Authorize]
        public async Task<Result<List<GetMessageDTO>>> GetMessages([FromQuery] GetMessagesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("{contactId}")]
        [Authorize]
        public async Task<Result<List<ContactMessagesDTO>>> GetPrivateMessages(long contactId)
        {
            return await Mediator.Send(new GetContactMessagesQuery() { ContactId = contactId });
        }

        [HttpGet]
        [Route("contacts")]
        [Authorize]
        public async Task<Result<ItemListDTO<UserContactsDTO>>> GetContacts([FromQuery] UserContactsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Route("upload")]
        [Authorize]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                var path = $"ChatFiles/{Guid.NewGuid()}";
                var success = await minIoService.UploadFile(file, path);
                if (!success)
                    return StatusCode(StatusCodes.Status500InternalServerError, "آپلود فایل با خطا مواجه شد!");

                return Ok(path);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "آپلود فایل با خطا مواجه شد!");
            }
        }
    }
}
