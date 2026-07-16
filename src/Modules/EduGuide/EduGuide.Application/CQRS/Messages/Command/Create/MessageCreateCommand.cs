using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EduGuide.Application.CQRS.Messages
{
    public class MessageCreateCommand
        : IRequest<Result<ReceiverDTO>>
    {
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public string Text { get; set; }
        public bool IsFile { get; set; } = false;
        public string FilePath { get; set; }
    }

    public class MessageCreateCommandHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager)
        : IRequestHandler<MessageCreateCommand, Result<ReceiverDTO>>
    {
        public async Task<Result<ReceiverDTO>> Handle(MessageCreateCommand request, CancellationToken cancellationToken)
        {
            var senderId = request.SenderId;
            var receiver = await uow.Users.FirstOrDefaultAsync(x => x.Id == request.ReceiverId);
            if (receiver == null)
                throw new HubException("کاربر موردنظر یافت نشد");

            if (senderId != 1)
                if (!await uow.RequestCounselors.AnyAsync(x => ((x.Student.UserId == senderId && x.Counselor.UserId == receiver.Id)
                || (x.Student.UserId == receiver.Id && x.Counselor.UserId == senderId)) && x.status == Domain.Enums.RequestStatus.Settlement))
                    throw new HubException("شما نمی‌توانید به این کاربر پیام دهید!");

            var message = new Message
            {
                Text = request.Text,
                SenderId = senderId,
                ReceiverId = receiver.Id,
                IsFile = request.IsFile,
                Path = request.FilePath,
            };
            await uow.Messages.AddAsync(message);
            await uow.CommitAsync();

            var receriverDto = new ReceiverDTO
            {
                Id = receiver.Id,
                FullName = receiver.FirstName + ' ' + receiver.LastName,
                MessageId = message.Id
            };

            return Result.Success(receriverDto);
        }
    }
}