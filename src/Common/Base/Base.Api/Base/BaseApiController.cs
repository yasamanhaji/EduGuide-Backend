using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Api.Base
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("policy")]
    public abstract class BaseApiController(IMediator mediator) : ControllerBase
    {
        private IMediator _mediator = mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}