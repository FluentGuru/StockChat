using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Application.Entities;
using Jobsity.StockChat.Application.Events;
using Jobsity.StockChat.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class ChatMessageSentEventHandler : INotificationHandler<ChatMessageSentEvent>
    {
        private readonly StockChatDbContext dbContext;
        private readonly IDateTime dateTime;

        public ChatMessageSentEventHandler(StockChatDbContext dbContext, IDateTime dateTime)
        {
            this.dbContext = dbContext;
            this.dateTime = dateTime;
        }

        public async Task Handle(ChatMessageSentEvent notification, CancellationToken cancellationToken)
        {
            var message = new ChatMessageEntity() { Stock = notification.Stock, FromNickName = notification.Nickname, Message = notification.Message, SentTime = dateTime.Now };
            await dbContext.AddAsync(message, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
