using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduGuide.Application.CQRS.Messages
{
    public class GetContactsQuery 
        : IRequest<Result<List<long>>>
    {
        public long CurrentUserId { get; set; }
    }

    public class GetContactsQueryHandler(IEduGuideUnitOfWork uow)
        : IRequestHandler<GetContactsQuery, Result<List<long>>>
    {
        public async Task<Result<List<long>>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var filter = PredicateBuilder.New<RequestCounselor>(true);

            if (request.CurrentUserId != 1)
                filter = filter.And(x => (x.Student.UserId == request.CurrentUserId || x.Counselor.UserId == request.CurrentUserId)
                    && x.status == RequestStatus.Settlement);

            var contacts = await uow.RequestCounselors.GetAsync(
                filter,
                includes: x => x.Include(x => x.Counselor).Include(x => x.Student));

            var contactUserIds = contacts
                .SelectMany(x => new[] { x.Student.UserId, x.Counselor.UserId })
                .Where(id => id != request.CurrentUserId)
                .Distinct()
                .ToList();

            return Result.Success(contactUserIds);
        }
    }
}
