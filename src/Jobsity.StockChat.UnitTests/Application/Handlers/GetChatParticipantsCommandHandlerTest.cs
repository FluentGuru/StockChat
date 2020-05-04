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
    public class GetChatParticipantsCommandHandlerTest
    {
        [Test]
        public async Task ShouldGetAllChatParticipants()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldGetAllChatParticipants));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("janedoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("foo", "johndoe"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetParticipant("foo", "johndoe"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetParticipant("foo", "janedoe"));
            var handler = new GetChatParticipantsCommandHandler(unitOfWork);

            var participants = await handler.Handle(new GetChatParticipantsCommand("foo"), default);

            Assert.AreEqual(2, participants.Count());
        }

        [Test]
        public async Task ShouldSortParticipantsAlphabetically()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldSortParticipantsAlphabetically));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("janedoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("foo", "johndoe"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetParticipant("foo", "johndoe"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetParticipant("foo", "janedoe"));
            var handler = new GetChatParticipantsCommandHandler(unitOfWork);

            var participants = await handler.Handle(new GetChatParticipantsCommand("foo"), default);

            Assert.AreEqual(2, participants.Count());
            Assert.AreEqual("janedoe", participants.First().Nickname);
            Assert.AreEqual("johndoe", participants.Last().Nickname);
        }
    }
}
