using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Infrastructure.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SubscribeToChat(string stock)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, stock);
        }
    }
}
