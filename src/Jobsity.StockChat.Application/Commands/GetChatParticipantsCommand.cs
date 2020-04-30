using Jobsity.StockChat.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class GetChatParticipantsCommand : ChatCommandBase, IRequest<IEnumerable<ChatParticipant>>
    {
        public GetChatParticipantsCommand(string stock) : base(stock)
        {
        }
    }
}
