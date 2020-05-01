using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Constants;
using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Domain.Exceptions;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserAuthentication>
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

        public async Task<UserAuthentication> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.GetUser(request.Nickname, cancellationToken);
            var hash = hasher.Generate(request.Password, user.PasswordSalt);
            if(user.PasswordSalt == hash)
            {
                user.PasswordSalt = hasher.GetSalt(UserAuthConstants.PasswordSaltLength);
                user.LastLoginDate = dateTime.Now;
                dbContext.Update(user);
                var token = new UserToken() { Token = hasher.Generate(user.LastLoginDate.ToString(), request.Nickname), CreatedDate = user.LastLoginDate, ExpirationDate = user.LastLoginDate.Add(request.TokenExpirationTime) };
                await dbContext.AddAsync(token);
                await dbContext.SaveChangesAsync(cancellationToken);
                return new UserAuthentication() { User = user, Token = token.Token };
            }

            throw new UserAuthenticationFailedException($"incorrect password");
        }
    }
}
