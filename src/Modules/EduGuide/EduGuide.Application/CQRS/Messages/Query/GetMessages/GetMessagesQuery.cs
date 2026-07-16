using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using MediatR;

namespace EduGuide.Application.CQRS.Messages
{
    public class GetMessagesQuery
        : IRequest<Result<List<GetMessageDTO>>>
    {
    }

    public class GetMessagesQueryHandler(IEduGuideUnitOfWork uow, IJwtManager jwtManager, IMinIoService minIoService)
        : IRequestHandler<GetMessagesQuery, Result<List<GetMessageDTO>>>
    {
        public async Task<Result<List<GetMessageDTO>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var senderId = jwtManager.GetUserId();
            var messages = await uow.Messages.GetAsync(x => x.SenderId == senderId);


            var groupedMessages = messages
                .GroupBy(m => m.ReceiverId)
                .Select(g => new GetMessageDTO
                {
                    Id = g.First().Id,
                    ContactId = g.Key,
                    Messages = g.Select(m => m.Text).ToList(),
                    SenderId = senderId.Value,
                })
                .ToList();

            foreach (var message in groupedMessages)
            {
                var user = await uow.Users.GetByIdAsync(message.ContactId);
                message.ContactFullName = user.FirstName + ' ' + user.LastName;
                if (user.Role == "Student")
                {
                    var student = await uow.Students.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    message.ContactPicUrl = await minIoService.GetDownloadUrl(student.PicName, $"Student/Profile/{user.FirstName + ' ' + user.LastName}/");
                }
                else if (user.Role == "Counselor")
                {
                    var counselor = await uow.Counselors.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    message.ContactPicUrl = await minIoService.GetDownloadUrl(counselor.ProfilePicName, $"Counselor/Profile/{user.FirstName + ' ' + user.LastName}/");
                }

            }


            return Result.Success(groupedMessages);
        }
    }
}