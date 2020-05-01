using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Events
{
    public class UserLeftChatEvent : ChatActionEventBase
    {
        public UserLeftChatEvent(string nickname, string stock) : base(nickname, stock)
        {
        }
    }
}
