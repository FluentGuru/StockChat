using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Events
{
    public class UserJoinedChatEvent : ChatMessageSentEvent
    {
        public UserJoinedChatEvent(string nickname, string stock) : base($"'{nickname}' has joined the chat", nickname, stock)
        {
        }
    }
}
