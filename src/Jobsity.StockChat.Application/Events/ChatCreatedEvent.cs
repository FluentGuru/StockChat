using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Events
{
    public class ChatCreatedEvent : ChatEventBase
    {
        public ChatCreatedEvent(string stock) : base(stock)
        {
        }
    }
}
