using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Events
{
    public class ChatEventBase : INotification
    {
        public ChatEventBase(string stock)
        {
            Stock = stock;
        }

        public string Stock { get; }
    }
}
