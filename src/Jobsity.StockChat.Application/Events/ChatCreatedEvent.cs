using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Events
{
    public class ChatCreatedEvent : ChatMessageSentEvent
    {
        public ChatCreatedEvent(string nickname, string stock) : base($"'{stock}' created", nickname, stock)
        {
        }
    }
}
