using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Handlers;
using Jobsity.StockChat.Domain.Entities;
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
    public class CreateUserCommandHandlerTest
    {
        [Test]
        public async Task ShouldFailIfAttemptingToCreateExistingUser()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldFailIfAttemptingToCreateExistingUser));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            await unitOfWork.AddAndSaveAsync(EntityFactory.GetUser("johndoe", "Pass@123"));
            var handler = new CreateUserCommandHandler(unitOfWork, InfrastructureFactory.GetMachineDateTime(), InfrastructureFactory.GetSha1Hasher());

            Assert.ThrowsAsync<CreatingExistingUserException>(() => handler.Handle(new CreateUserCommand("johndoe", "Pass@123"), default));
        }

        [Test]
        public async Task ShouldCreateNewUser()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldCreateNewUser));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var handler = new CreateUserCommandHandler(unitOfWork, InfrastructureFactory.GetMachineDateTime(), InfrastructureFactory.GetSha1Hasher());

            await handler.Handle(new CreateUserCommand("johndoe", "Pass@123"), default);

            var user = unitOfWork.GetSingleAsync<UserEntity>(u => u.Nickname == "johndoe");
            Assert.NotNull(user);
        }
    }
}
