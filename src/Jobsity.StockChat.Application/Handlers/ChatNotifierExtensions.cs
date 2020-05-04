using Jobsity.StockChat.Domain.Constants;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public static class ChatNotifierExtensions
    {
        public static Task NotifyMessageSet(this IChatNotifier notifier, ChatMessage message)
        {
            return notifier.NotifyAsync(message.Stock, ChatConstants.MessageSentActionName, message);
        }

        public static Task NotifyUserJoinedChat(this IChatNotifier notifier, string stock, string nickname)
        {
            return notifier.NotifyAsync(stock, ChatConstants.UserJoinedActionName, nickname);
        }
    }
}
