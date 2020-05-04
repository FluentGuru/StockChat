using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Handlers;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.UnitTests.Commons;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.UnitTests.Application.Handlers
{
    public class GetChatMessagesCommandHandlerTest
    {
        [Test]
        public async Task ShouldGetChatMessages()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldGetChatMessages));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("foo", "johndoe"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChatMessage("foo", "johndoe", "Msg 1", DateTime.Now));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChatMessage("foo", "johndoe", "Msg 2", DateTime.Now));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChatMessage("foo", "johndoe", "Msg 3", DateTime.Now));
            var handler = new GetChatMessagesCommandHandler(unitOfWork);

            var messages = await handler.Handle(new GetChatMessagesCommand("foo"), default);

            Assert.AreEqual(3, messages.Count());
        }

        [Test]
        public async Task ShouldSortMessagesBySentTime()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldSortMessagesBySentTime));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("foo", "johndoe"));
            await unitOfWork.CreateChatMessages("foo", "owner", "Message", 2, new DateTime(2020, 5, 4));
            var handler = new GetChatMessagesCommandHandler(unitOfWork);

            var messages = await handler.Handle(new GetChatMessagesCommand("foo"), default);

            Assert.AreEqual(2, messages.Count());
            Assert.AreEqual(6, messages.First().SentTime.Day);
            Assert.AreEqual(5, messages.Last().SentTime.Day);
        }

        [Test]
        public async Task ShouldOnlyReturnSpecifiedNumberOfMessages()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldOnlyReturnSpecifiedNumberOfMessages));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("foo", "johndoe"));
            await unitOfWork.CreateChatMessages("foo", "owner", "Message", 12, new DateTime(2020, 5, 4));
            var handler = new GetChatMessagesCommandHandler(unitOfWork);

            var messages = await handler.Handle(new GetChatMessagesCommand("foo", 10), default);

            Assert.AreEqual(10, messages.Count());
        }
    }

    internal static class UnitOfWorkExtension
    {
        public static async Task CreateChatMessages(this IUnitOfWork unitOfWork, string stock, string owner, string message, int count, DateTime sentTime)
        {
            for (int i = 1; i <= count; i++)
            {
                await unitOfWork.AddAsync(EntityFactory.GetChatMessage(stock, owner, $"{message} {i}", sentTime.AddDays(i)));
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}
