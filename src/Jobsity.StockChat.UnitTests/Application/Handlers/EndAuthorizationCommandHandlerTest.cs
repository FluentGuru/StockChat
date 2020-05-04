using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Handlers;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.UnitTests.Commons;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.UnitTests.Application.Handlers
{
    public class EndAuthorizationCommandHandlerTest
    {
        [Test]
        public async Task ShouldExpireToken()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldExpireToken));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetToken("TOKEN", "johndoe", DateTime.Now));
            var handler = new EndAuthorizationCommandHandler(unitOfWork);

            await handler.Handle(new EndAuthorizationCommand("TOKEN"), default);

            var token = await unitOfWork.GetSingleAsync<UserTokenEntity>(t => t.Token == "TOKEN");
            Assert.AreEqual(DateTime.MinValue, token.ExpirationDate);
        }
    }
}
