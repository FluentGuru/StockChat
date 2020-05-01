using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Events
{
    public abstract class ChatActionEventBase : ChatEventBase
    {
        public ChatActionEventBase(string nickname, string stock) : base(stock)
        {
            Nickname = nickname;
        }

        public string Nickname { get; }
    }
}
