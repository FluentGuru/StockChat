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
    public class GetChatParticipantsCommandHandler : IRequestHandler<GetChatParticipantsCommand, IEnumerable<ChatParticipant>>
    {
        private readonly StockChatDbContext dbContext;

        public GetChatParticipantsCommandHandler(StockChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ChatParticipant>> Handle(GetChatParticipantsCommand request, CancellationToken cancellationToken)
        {
            return await dbContext.ChatParticipants.Where(p => p.Stock == request.Stock).OrderBy(p => p.Nickname).ToListAsync(cancellationToken);
        }
    }
}
