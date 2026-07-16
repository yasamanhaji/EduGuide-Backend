using EduGuide.Application.Contracts.Repositories;
using LinqKit;
using MediatR;

namespace EduGuide.Application.CQRS.Messages
{
    public class UnseenMessagesQuery
        : IRequest<List<long>>
    {
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
    }

    public class UnseenMessagesQueryHandler(IEduGuideUnitOfWork uow)
        : IRequestHandler<UnseenMessagesQuery, List<long>>
    {
        public async Task<List<long>> Handle(UnseenMessagesQuery request, CancellationToken cancellationToken)
        {
            var UnseenMessages = await uow.Messages.GetAsync(x => x.Seen == false && x.ReceiverId == request.ReceiverId && x.SenderId == request.SenderId);
            UnseenMessages.ForEach(x => x.Seen = true);
            await uow.CommitAsync();

            return UnseenMessages.Select(x => x.Id).ToList();
        }
    }
}