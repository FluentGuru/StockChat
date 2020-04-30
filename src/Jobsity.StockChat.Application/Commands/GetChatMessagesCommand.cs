using Jobsity.StockChat.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class GetChatMessagesCommand : ChatCommandBase, IRequest<IEnumerable<ChatMessage>>
    {
        public GetChatMessagesCommand(string stock) : base(stock)
        {
        }
    }
}
