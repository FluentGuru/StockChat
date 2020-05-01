using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
    {
        private readonly IMediator mediator;

        public SendMessageCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            await mediator.Publish(new ChatMessageSentEvent(request.Message, request.Nickname, request.Stock), cancellationToken);
            return Unit.Value;
        }
    }
}
