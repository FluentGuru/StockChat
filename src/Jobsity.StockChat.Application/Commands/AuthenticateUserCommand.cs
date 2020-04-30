using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class AuthenticateUserCommand : IRequest<bool>
    {
        public AuthenticateUserCommand(string nickname, string password)
        {
            Nickname = nickname;
            Password = password;
        }

        public string Nickname { get; }
        public string Password { get; }
    }
}
