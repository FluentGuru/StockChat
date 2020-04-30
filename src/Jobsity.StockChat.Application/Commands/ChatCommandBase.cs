using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Commands
{
    public abstract class ChatCommandBase : IRequest
    {
        public ChatCommandBase(string stock)
        {
            Stock = stock;
        }

        public string Stock { get; }
    }
}
