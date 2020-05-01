using Jobsity.StockChat.Application.Constants;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class GetChatMessagesCommand : ChatCommandBase, IRequest<IEnumerable<ChatMessage>>
    {
        public GetChatMessagesCommand(string stock, int count = ChatConstants.FetchMessagesCount) : base(stock)
        {
            Count = count;
        }

        public int Count { get; }
    }
}
