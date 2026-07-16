using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using Base.Application.Exceptions;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using MediatR;

namespace EduGuide.Application.CQRS.RequestCounselors.Command.Delete
{
    public class RequestDeleteCommand:IRequest<Result<bool>>
    {

    }
    public class RequestDeleteCommandHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager) : IRequestHandler<RequestDeleteCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(RequestDeleteCommand request, CancellationToken cancellationToken)
        {
            var Id=jwtManager.GetUserId();
            var requestCounselor = await uow.RequestCounselors
                .FirstOrDefaultAsync(s => s.Student.UserId == Id && s.status== RequestStatus.Requested);
            if (requestCounselor == null)
            {
                throw new Exception("درخواستی با وضعیت «در حال بررسی» برای حذف یافت نشد");
            }
            requestCounselor.IsDeleted = true;
            await uow.CommitAsync();
            return Result.Success(true);


        }
    }
}
