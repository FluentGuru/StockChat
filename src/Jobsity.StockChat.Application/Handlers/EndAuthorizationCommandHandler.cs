using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class EndAuthorizationCommandHandler : IRequestHandler<EndAuthorizationCommand>
    {
        private readonly StockChatDbContext dbContext;

        public EndAuthorizationCommandHandler(StockChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(EndAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var token = await dbContext.UserTokens.FirstAsync(t => t.Token == request.Token, cancellationToken);
            if(token != null)
            {
                token.ExpirationDate = DateTime.MinValue;
                dbContext.Update(token);
                await dbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

            throw new TokenNotFoundException("Invalid token or authorization");
        }
    }
}
