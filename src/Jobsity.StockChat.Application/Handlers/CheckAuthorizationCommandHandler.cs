using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class CheckAuthorizationCommandHandler : IRequestHandler<CheckAuthorizationCommand, bool>
    {
        private readonly StockChatDbContext dbContext;
        private readonly IDateTime dateTime;

        public CheckAuthorizationCommandHandler(StockChatDbContext dbContext, IDateTime dateTime)
        {
            this.dbContext = dbContext;
            this.dateTime = dateTime;
        }

        public async Task<bool> Handle(CheckAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var now = dateTime.Now;
            return await dbContext.UserTokens.AnyAsync(t => t.Token == request.Token && t.ExpirationDate > now);
        }
    }
}
