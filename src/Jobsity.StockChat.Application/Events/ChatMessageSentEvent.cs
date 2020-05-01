using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Events
{
    public class ChatMessageSentEvent : ChatActionEventBase
    {
        public ChatMessageSentEvent(string message, string nickname, string stock) : base(nickname, stock)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
