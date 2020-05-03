using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Events;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class JoinChatCommandHandler : IRequestHandler<JoinChatCommand, Chat>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;
        private readonly IDateTime dateTime;

        public JoinChatCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, IDateTime dateTime)
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
            this.dateTime = dateTime;
        }

        public async Task<Chat> Handle(JoinChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await unitOfWork.GetSingleAsync<ChatEntity>(c => c.Stock == request.Stock);
            if(chat != null)
            {
                chat = new ChatEntity() { Stock = request.Stock, OwnerNickname = request.Nickname, CreateDate = dateTime.Now };
                await unitOfWork.AddAndSaveAsync(chat);
                await mediator.Publish(new ChatCreatedEvent(request.Nickname, request.Stock), cancellationToken);
            }
            if(!(await unitOfWork.AnyAsync<ChatParticipantEntity>(p => p.Stock == request.Stock && p.Nickname == request.Nickname)))
            {
                await unitOfWork.AddAndSaveAsync(new ChatParticipantEntity() { Stock = request.Stock, Nickname = request.Nickname });
                await mediator.Publish(new UserJoinedChatEvent(request.Nickname, request.Stock));
            }

            return chat;
        }
    }
}
