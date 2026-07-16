using Base.Application;
using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using EduGuide.Domain.Enums;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace EduGuide.Application.CQRS.Messages
{
    public class UserContactsQuery
        : IRequest<Result<ItemListDTO<UserContactsDTO>>>
    {
        [BindNever]
        public long CurrentUserId { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
    }

    public class UserContactsQueryHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager, IMinIoService minIoService)
        : IRequestHandler<UserContactsQuery, Result<ItemListDTO<UserContactsDTO>>>
    {
        public async Task<Result<ItemListDTO<UserContactsDTO>>> Handle(UserContactsQuery request, CancellationToken cancellationToken)
        {
            var sort = "id desc";
            var filter = PredicateBuilder.New<RequestCounselor>(true);
            request.CurrentUserId = jwtManager.GetUserId().Value;
            var role = jwtManager.GetRole();

            //if (request.CurrentUserId != 1)
            //    filter = filter.And(x => (x.Student.UserId == request.CurrentUserId || x.Counselor.UserId == request.CurrentUserId)
            //                && x.status == RequestStatus.Settlement);

            //if (role == "Student")
            //    filter = filter.And(x => x.Student.UserId == request.CurrentUserId && x.status == RequestStatus.Settlement);
            //else if (role == "Counselor")
            //    filter = filter.And(x => x.Counselor.UserId == request.CurrentUserId && x.status == RequestStatus.Settlement);

            var model = new ItemListDTO<UserContactsDTO>();
            model.PageSize = request.PageSize;
            model.TotalCount = await uow.RequestCounselors.CountAsync();
            model.PageIndex = request.PageIndex;
            Expression<Func<RequestCounselor, UserContactsDTO>> selector = null;

            if (role == "Student")
            {
                filter = filter.And(x => x.Student.UserId == request.CurrentUserId && (x.status == RequestStatus.Settlement || x.status == RequestStatus.Ended));
                model.FilteredCount = await uow.RequestCounselors.CountAsync(filter);
                selector = UserContactsDTO.StudentSelector;
            }
            else if (role == "Counselor")
            {
                filter = filter.And(x => x.Counselor.UserId == request.CurrentUserId && (x.status == RequestStatus.Settlement || x.status == RequestStatus.Ended));
                model.FilteredCount = await uow.RequestCounselors.CountAsync(filter);
                selector = UserContactsDTO.CounselorSelector;
            }

            model.Items = (await uow.RequestCounselors.GetDTOAsync(
                selector,
                filter,
                orderBy: x => x.OrderBy(sort),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
                )).DistinctBy(x => x.ContactId).ToList();
            foreach (var item in model.Items)
            {
                item.ContactProfilePicUrl = await minIoService.GetDownloadUrl(item.PicName, $"{item.Role}/Profile/{item.ContactName}");
                item.LastMessage = (await uow.Messages.LastOrDefaultAsync(x => (x.ReceiverId == request.CurrentUserId && x.SenderId == item.ContactId)
                || (x.SenderId == request.CurrentUserId && x.ReceiverId == item.ContactId), orderBy: x => x.OrderBy("id asc")))?.Text;
            }

            return Result.Success(model);
        }
    }
}
