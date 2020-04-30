using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class LeaveChatCommand : ChatActionCommandBase
    {
        public LeaveChatCommand(string nickname, string stock) : base(nickname, stock)
        {
        }
    }
}
