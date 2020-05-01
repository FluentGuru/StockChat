using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Constants;
using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, bool>
    {
        private readonly StockChatDbContext dbContext;
        private readonly IHasher hasher;
        private readonly IDateTime dateTime;

        public AuthenticateUserCommandHandler(StockChatDbContext dbContext, IHasher hasher, IDateTime dateTime)
        {
            this.dbContext = dbContext;
            this.hasher = hasher;
            this.dateTime = dateTime;
        }

        public async Task<bool> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.GetUser(request.Nickname, cancellationToken);
            var hash = hasher.Generate(request.Password, user.PasswordSalt);
            if(user.PasswordSalt == hash)
            {
                user.PasswordSalt = hasher.GetSalt(UserAuthConstants.PasswordSaltLength);
                user.LastLoginDate = dateTime.Now;
                dbContext.Update(user);
                await dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }

            return false;
        }
    }
}
