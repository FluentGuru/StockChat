using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class JoinChatCommand : ChatActionCommandBase
    {
        public JoinChatCommand(string nickname, string stock) : base(nickname, stock)
        {
        }
    }
}
