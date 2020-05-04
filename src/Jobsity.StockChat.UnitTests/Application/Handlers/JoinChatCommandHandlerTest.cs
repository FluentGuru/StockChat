using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Handlers;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using Jobsity.StockChat.UnitTests.Commons;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.StockChat.UnitTests.Application.Handlers
{
    public class JoinChatCommandHandlerTest
    {
        [Test]
        public async Task ShouldMakeUserParticipantInChat()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldMakeUserParticipantInChat));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("admin", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetChat("foo", "admin"));
            var mediator = InfrastructureFactory.GetMediatorSubstitute();
            var handler = new JoinChatCommandHandler(unitOfWork, mediator, InfrastructureFactory.GetMachineDateTime());

            await handler.Handle(new JoinChatCommand("johndoe", "foo"), default);

            var pars = await unitOfWork.FetchAsync<ChatParticipantEntity, ChatParticipant>(
                participants =>
                participants.Where(p => p.Stock == "foo"));
            Assert.AreEqual(1, pars.Count());
            Assert.AreEqual("johndoe", pars.First().Nickname);
        }
    }
}
