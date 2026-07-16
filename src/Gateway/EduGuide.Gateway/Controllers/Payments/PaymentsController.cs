using Base.Api.Base;
using Base.Application;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.CQRS.Payments;
using EduGuide.Application.CQRS.Payments.Command.Settlement;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduGuide.Gateway.Controllers.Payments
{
    public class PaymentsController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet]
        [Authorize(Roles = "Student, Counselor")]
        public async Task<ActionResult<Result<ItemListDTO<PaymentListDTO>>>> GetList([FromQuery] PaymentGetListQuery query)
        {
            return await Mediator.Send(query);
        }
        
        [HttpPatch]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Result>> Settlement([FromQuery] PaymentSettlementCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Counselor")]
        public async Task<ActionResult<Result<ItemListDTO<CounselorPaymentsDTO>>>> CounselorPayments([FromQuery] CounselorPaymentsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}