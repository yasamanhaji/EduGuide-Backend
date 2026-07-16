using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.Contracts.Repositories;
using MediatR;

namespace EduGuide.Application.CQRS.Messages
{
    public class SeenCommand
        : IRequest<Result>
    {
        public List<long> MessageIds { get; set; }
    }

    public class SeenCommandHandler(IEduGuideUnitOfWork uow)
        : IRequestHandler<SeenCommand, Result>
    {
        public async Task<Result> Handle(SeenCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.MessageIds)
            {
                var message = await uow.Messages.GetByIdAsync(id);

                message.Seen = true;
                await uow.CommitAsync();
            }

            return Result.Success();
        }
    }
}
