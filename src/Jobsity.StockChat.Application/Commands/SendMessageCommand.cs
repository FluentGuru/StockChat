using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class SendMessageCommand : ChatActionCommandBase
    {
        public SendMessageCommand(string message, string nickname, string stock) : base(nickname, stock)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
