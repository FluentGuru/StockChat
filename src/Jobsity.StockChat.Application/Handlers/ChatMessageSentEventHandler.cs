﻿using Jobsity.StockChat.Application.Events;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class ChatMessageSentEventHandler : INotificationHandler<ChatMessageSentEvent>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTime dateTime;
        private readonly IChatNotifier notifier;

        public ChatMessageSentEventHandler(IUnitOfWork unitOfWork, IDateTime dateTime, IChatNotifier notifier)
        {
            this.unitOfWork = unitOfWork;
            this.dateTime = dateTime;
            this.notifier = notifier;
        }

        public async Task Handle(ChatMessageSentEvent notification, CancellationToken cancellationToken)
        {
            var message = new ChatMessageEntity() { Stock = notification.Stock, FromNickName = notification.Nickname, Message = notification.Message, SentTime = dateTime.Now };
            await unitOfWork.AddAndSaveAsync(message);
            await notifier.NotifyMessageSet(message);
        }
    }
}
