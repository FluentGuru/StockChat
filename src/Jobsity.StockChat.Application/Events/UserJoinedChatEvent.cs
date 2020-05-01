using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Events
{
    public class UserJoinedChatEvent : ChatActionEventBase
    {
        public UserJoinedChatEvent(string nickname, string stock) : base(nickname, stock)
        {
        }
    }
}
