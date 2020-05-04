using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Handlers;
using Jobsity.StockChat.Domain.Exceptions;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.UnitTests.Commons;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.UnitTests.Application.Handlers
{
    public class GetUserCommandHandlerTest
    {
        [Test]
        public async Task ShouldReturnUser()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldReturnUser));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            var handler = new GetUserCommandHandler(unitOfWork);

            var user = await handler.Handle(new GetUserCommand("johndoe"), default);

            Assert.NotNull(user);
        }

        [Test]
        public void ShouldFailIfUserDoesNotExists()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldFailIfUserDoesNotExists));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var handler = new GetUserCommandHandler(unitOfWork);

            Assert.ThrowsAsync<UserNotFoundException>(() => handler.Handle(new GetUserCommand("johndoe"), default));
        }
    }
}
