using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class GetChatMessagesCommandHandler : IRequestHandler<GetChatMessagesCommand, IEnumerable<ChatMessage>>
    {
        private readonly StockChatDbContext dbContext;

        public GetChatMessagesCommandHandler(StockChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ChatMessage>> Handle(GetChatMessagesCommand request, CancellationToken cancellationToken)
        {
            return await dbContext.ChatMessages
                .Where(m => m.Stock == request.Stock)
                .OrderByDescending(m => m.SentTime)
                .Take(request.Count)
                .ToListAsync(cancellationToken);
        }
    }
}
