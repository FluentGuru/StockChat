using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Domain.Exceptions;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand, User>
    {
        private readonly StockChatDbContext dbContext;

        public GetUserCommandHandler(StockChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            return await dbContext.GetUser(request.Nickname, cancellationToken);
        }
    }
}
