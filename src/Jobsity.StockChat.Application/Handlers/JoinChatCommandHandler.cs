using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Application.Entities;
using Jobsity.StockChat.Application.Events;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class JoinChatCommandHandler : IRequestHandler<JoinChatCommand, Chat>
    {
        private readonly StockChatDbContext dbContext;
        private readonly IMediator mediator;
        private readonly IDateTime dateTime;

        public JoinChatCommandHandler(StockChatDbContext dbContext, IMediator mediator, IDateTime dateTime)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.dateTime = dateTime;
        }

        public async Task<Chat> Handle(JoinChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await dbContext.FindAsync<ChatEntity>(request.Stock, cancellationToken);
            if(chat != null)
            {
                chat = new ChatEntity() { Stock = request.Stock, OwnerNickname = request.Nickname, CreateDate = dateTime.Now };
                await dbContext.AddAsync(chat, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new ChatCreatedEvent(request.Nickname, request.Stock), cancellationToken);
            }
            if(!(await dbContext.ChatParticipants.AnyAsync(p => p.Stock == request.Stock && p.Nickname == request.Nickname)))
            {
                await dbContext.AddAsync(new ChatParticipantEntity() { Stock = request.Stock, Nickname = request.Nickname }, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new UserJoinedChatEvent(request.Nickname, request.Stock));
            }

            return chat;
        }
    }
}
