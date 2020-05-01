using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public class EndAuthorizationCommand : IRequest
    {
        public EndAuthorizationCommand(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
