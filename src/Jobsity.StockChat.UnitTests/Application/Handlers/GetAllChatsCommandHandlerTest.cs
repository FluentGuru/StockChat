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
    public class GetAllChatsCommandHandlerTest
    {
        [Test]
        public async Task ShouldGetAllTheChats()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldGetAllTheChats));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("chat1", "johndoe"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("chat2", "johndoe"));
            var handler = new GetAllChatsCommandHandler(unitOfWork);

            var chats = await handler.Handle(new GetAllChatsCommand(), default);

            Assert.AreEqual(2, chats.Count());

        }

        [Test]
        public async Task ShouldOrderChatsAlphabetically()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldGetAllTheChats));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("foo", "johndoe"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("bar", "johndoe"));
            var handler = new GetAllChatsCommandHandler(unitOfWork);

            var chats = await handler.Handle(new GetAllChatsCommand(), default);

            Assert.AreEqual(2, chats.Count());
            Assert.AreEqual("bar", chats.First().Stock);
            Assert.AreEqual("foo", chats.Last().Stock);
        }
    }
}
