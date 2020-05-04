using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Handlers;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.UnitTests.Commons;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.UnitTests.Application.Handlers
{
    public class CheckAuthorizationCommandHandlerTest
    {
        [Test]
        public async Task ShouldReturnTrueIfTokenHasNotExpired()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldReturnTrueIfTokenHasNotExpired));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var dateTime = new TransientDateTime();
            dateTime.Now = new DateTime(2020, 5, 3);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetToken("TOKEN", "johndoe", dateTime.Now.AddDays(1)));
            var handler = new CheckAuthorizationCommandHandler(unitOfWork, dateTime);

            Assert.IsTrue(await handler.Handle(new CheckAuthorizationCommand("TOKEN"), default));

        }

        [Test]
        public async Task ShouldReturnFalseIfTokenHasExpired()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldReturnFalseIfTokenHasExpired));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var dateTime = new TransientDateTime();
            dateTime.Now = new DateTime(2020, 5, 3);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetToken("TOKEN", "johndoe", dateTime.Now.AddDays(1)));
            var handler = new CheckAuthorizationCommandHandler(unitOfWork, dateTime);

            dateTime.Now = dateTime.Now.AddMonths(1);

            Assert.IsFalse(await handler.Handle(new CheckAuthorizationCommand("TOKEN"), default));
        }
    }
}
