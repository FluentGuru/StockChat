using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class CreateChatCommand : ChatActionCommandBase
    {
        public CreateChatCommand(string nickname, string stock) : base(nickname, stock)
        {
        }
    }
}
