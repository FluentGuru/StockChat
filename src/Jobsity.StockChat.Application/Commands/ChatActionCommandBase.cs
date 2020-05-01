using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public abstract class ChatActionCommandBase : ChatCommandBase
    {
        public ChatActionCommandBase(string nickname, string stock) : base(stock)
        {
            Nickname = nickname;
        }

        public string Nickname { get; }
    }
}
