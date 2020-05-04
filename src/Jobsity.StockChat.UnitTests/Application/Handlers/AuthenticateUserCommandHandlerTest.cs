using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Handlers;
using Jobsity.StockChat.Domain.Constants;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Exceptions;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.UnitTests.Commons;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.UnitTests.Application.Handlers
{
    public class AuthenticateUserCommandHandlerTest
    {
        [Test]
        public async Task ShouldReturnAuthenticatedUser()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldReturnAuthenticatedUser));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var user = EntityFactory.GetUser("johndoe", "Pass@123");
            await unitOfWork.AddAndSaveAsync(user);

            var handler = new AuthenticateUserCommandHandler(unitOfWork, InfrastructureFactory.GetSha1Hasher(), InfrastructureFactory.GetMachineDateTime());
            var authentication = await handler.Handle(new AuthenticateUserCommand("johndoe", "Pass@123", UserAuthConstants.TokenExpirationTime), default);

            Assert.AreEqual("johndoe", authentication.User.Nickname);
            Assert.IsFalse(string.IsNullOrEmpty(authentication.Token));
        }

        [Test]
        public async Task ShouldFailAuthenticationIfWrongPassword()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldFailAuthenticationIfWrongPassword));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var user = EntityFactory.GetUser("johndoe", "Pass@123");
            await unitOfWork.AddAndSaveAsync(user);

            var handler = new AuthenticateUserCommandHandler(unitOfWork, InfrastructureFactory.GetSha1Hasher(), InfrastructureFactory.GetMachineDateTime());

            Assert.ThrowsAsync<UserAuthenticationFailedException>(() => handler.Handle(new AuthenticateUserCommand("johndoe", "Pass@13", UserAuthConstants.TokenExpirationTime), default));
        }

        [Test]
        public async Task ShouldChangePasswordSaltAndReHashPasswordOnSuccesfulAuth()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldChangePasswordSaltAndReHashPasswordOnSuccesfulAuth));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var user = EntityFactory.GetUser("johndoe", "Pass@123");
            await unitOfWork.AddAndSaveAsync(user);
            var salt = user.PasswordSalt;
            var passwordHash = user.PasswordHash;
            var lastLoginDate = user.LastLoginDate;

            var handler = new AuthenticateUserCommandHandler(unitOfWork, InfrastructureFactory.GetSha1Hasher(), InfrastructureFactory.GetMachineDateTime());
            var authentication = await handler.Handle(new AuthenticateUserCommand("johndoe", "Pass@123", UserAuthConstants.TokenExpirationTime), default);

            user = await unitOfWork.GetSingleAsync<UserEntity>(u => u.Nickname == "johndoe");
            Assert.AreNotEqual(salt, user.PasswordSalt);
            Assert.AreNotEqual(passwordHash, user.PasswordSalt);
            Assert.AreNotEqual(lastLoginDate, user.LastLoginDate);
        }

        [Test]
        public async Task ShouldCreateTokenOnSucesfulAuth()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldChangePasswordSaltAndReHashPasswordOnSuccesfulAuth));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var user = EntityFactory.GetUser("johndoe", "Pass@123");
            await unitOfWork.AddAndSaveAsync(user);

            var handler = new AuthenticateUserCommandHandler(unitOfWork, InfrastructureFactory.GetSha1Hasher(), InfrastructureFactory.GetMachineDateTime());
            var authentication = await handler.Handle(new AuthenticateUserCommand("johndoe", "Pass@123", UserAuthConstants.TokenExpirationTime), default);

            var tokenCount = await dbContext.UserTokens.CountAsync(u => u.Nickname == "johndoe");
            Assert.AreEqual(1, tokenCount);
        }
    }
}
