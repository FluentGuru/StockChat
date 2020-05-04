using Jobsity.StockChat.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class GetAllChatsCommand : IRequest<IEnumerable<Chat>>
    {
    }
}
