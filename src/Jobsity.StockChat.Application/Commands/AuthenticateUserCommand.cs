using Jobsity.StockChat.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class AuthenticateUserCommand : IRequest<UserAuthentication>
    {
        public AuthenticateUserCommand(string nickname, string password, TimeSpan tokenExpirationTime)
        {
            Nickname = nickname;
            Password = password;
            TokenExpirationTime = tokenExpirationTime;
        }

        public string Nickname { get; }
        public string Password { get; }
        public TimeSpan TokenExpirationTime { get; }
    }
}
