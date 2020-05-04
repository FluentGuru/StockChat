using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Domain.Constants;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Exceptions;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserAuthentication>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHasher hasher;
        private readonly IDateTime dateTime;

        public AuthenticateUserCommandHandler(IUnitOfWork unitOfWork, IHasher hasher, IDateTime dateTime)
        {
            this.unitOfWork = unitOfWork;
            this.hasher = hasher;
            this.dateTime = dateTime;
        }

        public async Task<UserAuthentication> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.GetUser(request.Nickname);
            var hash = hasher.Generate(request.Password, user.PasswordSalt);
            if(user.PasswordHash == hash)
            {
                user.PasswordSalt = hasher.GetSalt(UserAuthConstants.PasswordSaltLength);
                user.PasswordHash = hasher.Generate(request.Password, user.PasswordSalt);
                user.LastLoginDate = dateTime.Now;
                await unitOfWork.UpdateAsync(user);
                var token = new UserTokenEntity() { Token = hasher.Generate(user.LastLoginDate.ToString(), request.Nickname), Nickname = request.Nickname, CreatedDate = user.LastLoginDate, ExpirationDate = user.LastLoginDate.Add(request.TokenExpirationTime) };
                await unitOfWork.AddAndSaveAsync(token);
                return new UserAuthentication() { User = user, Token = token.Token };
            }

            throw new UserAuthenticationFailedException($"incorrect password");
        }
    }
}
