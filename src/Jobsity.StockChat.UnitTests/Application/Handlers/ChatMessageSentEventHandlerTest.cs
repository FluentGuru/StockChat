using Jobsity.StockChat.Application.Events;
using Jobsity.StockChat.Application.Handlers;
using Jobsity.StockChat.Domain.Constants;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using Jobsity.StockChat.UnitTests.Commons;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.UnitTests.Application.Handlers
{
    public class ChatMessageSentEventHandlerTest 
    {
        [Test]
        public async Task ShouldSaveMessageSent()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldSaveMessageSent));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("test", "johndoe"));
            var handler = new ChatMessageSentEventHandler(unitOfWork, InfrastructureFactory.GetMachineDateTime(), InfrastructureFactory.GetNotifierSubstitute());

            await handler.Handle(new ChatMessageSentEvent("Test Message", "johndoe", "test"), default);

            var message = await unitOfWork.GetSingleAsync<ChatMessageEntity>(m => m.Stock == "test");
            Assert.AreEqual("johndoe", message.FromNickName);
            Assert.AreEqual("Test Message", message.Message);
        }

        [Test]
        public async Task ShouldNotifyChatMessageSent()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldNotifyChatMessageSent));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("test", "johndoe"));
            var notifier = InfrastructureFactory.GetNotifierSubstitute();
            var handler = new ChatMessageSentEventHandler(unitOfWork, InfrastructureFactory.GetMachineDateTime(), notifier);

            await handler.Handle(new ChatMessageSentEvent("Test Message", "johndoe", "test"), default);

            await notifier.Received(1).NotifyAsync("test", ChatConstants.MessageSentActionName, Arg.Any<ChatMessage>());
        }
    }
}
