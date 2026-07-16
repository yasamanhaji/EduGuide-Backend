using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using FarsiLibrary.Utils;
using MediatR;

namespace EduGuide.Application.CQRS.Messages
{
    public class GetContactMessagesQuery
        : IRequest<Result<List<ContactMessagesDTO>>>
    {
        public long ContactId { get; set; }
    }

    public class GetContactMessagesQueryHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager, IMinIoService minIoService)
        : IRequestHandler<GetContactMessagesQuery, Result<List<ContactMessagesDTO>>>
    {
        public async Task<Result<List<ContactMessagesDTO>>> Handle(GetContactMessagesQuery request, CancellationToken cancellationToken)
        {
            var senderId = jwtManager.GetUserId();

            var messages = await uow.Messages.GetAsync(
                x => (x.SenderId == senderId && x.ReceiverId == request.ContactId) || (x.SenderId == request.ContactId && x.ReceiverId == senderId)
                );

            foreach (var message in messages)
            {
                if (message.ReceiverId == senderId)
                    message.Seen = true;
            }
            await uow.CommitAsync();

            var models = messages.Select(x => new ContactMessagesDTO
            {
                Id = x.Id,
                ReceiverId = x.ReceiverId,
                SenderId = x.SenderId,
                Text = x.Text,
                SendDate = $"{PersianDateConverter.ToPersianDate(x.CreateDate):yyyy/MM/dd} " + $"{x.CreateDate.AddHours(3).AddMinutes(30):HH:mm}",
                Seen = x.Seen,
                IsFile = x.IsFile, 
                FilePath = x.Path
            }).ToList();
            
            foreach (var model in models)
            {
                if (model.IsFile)
                    model.FileUrl = await minIoService.GetDownloadUrl(model.Text, model.FilePath);
            }

            return Result.Success(models);
        }
    }
}