using Jobsity.StockChat.Infrastructure.Hubs;
using Jobsity.StockChat.Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.UnitTests.Infrastructure
{
    public class ChatHubNotifierTest
    {
        [Test]
        public void ShouldNotifyTheGroupOfClientsOnTheHub()
        {
            var hubContext = GetHubContextForGroup();
            var notifier = new ChatHubNotifier(hubContext);

            notifier.NotifyAsync("test", "Test", "test");

            hubContext.Clients.Group("test").Received(1);
        }

        private IHubContext<ChatHub> GetHubContextForGroup()
        {
            var hubContext = Substitute.For<IHubContext<ChatHub>>();
            hubContext.Clients.Returns(Substitute.For<IHubClients>());
            return hubContext;
        }
    }
}
