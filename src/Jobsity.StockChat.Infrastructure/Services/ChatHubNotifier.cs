using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Infrastructure.Services
{
    public class ChatHubNotifier : IChatNotifier
    {
        private readonly IHubContext<ChatHub> hubContext;

        public ChatHubNotifier(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public Task NotifyAsync<T>(string group, string action, T data)
        {
            return hubContext.Clients.Group(group).SendAsync(action, data);
        }
    }
}
