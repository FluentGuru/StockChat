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
    public class UserJoinedChatEventHandler : INotificationHandler<UserJoinedChatEvent>
    {
        private readonly IChatNotifier notifier;

        public UserJoinedChatEventHandler(IChatNotifier notifier)
        {
            this.notifier = notifier;
        }
        public Task Handle(UserJoinedChatEvent notification, CancellationToken cancellationToken)
        {
            return notifier.NotifyUserJoinedChat(notification.Stock, notification.Nickname);
        }
    }
}
