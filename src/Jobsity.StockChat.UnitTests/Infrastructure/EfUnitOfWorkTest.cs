using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Infrastructure.Data;
using Jobsity.StockChat.Infrastructure.Services;
using Jobsity.StockChat.UnitTests.Commons;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.UnitTests.Infrastructure
{
    public class EfUnitOfWorkTest
    {
        [Test]
        public async Task ShouldAddEntityToDbContext()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldAddEntityToDbContext));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var user = EntityFactory.GetUser("johndoe", "Pass@123");
            await unitOfWork.AddAndSaveAsync(user);
            user = await dbContext.Users.FindAsync("johndoe");
            Assert.NotNull(user);
            Assert.AreEqual("johndoe", user.Nickname);
        }

        [Test]
        public async Task ShouldUpdateEntityOnDbContext()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldUpdateEntityOnDbContext));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var user = EntityFactory.GetUser("johndoe", "Pass@123");
            await unitOfWork.AddAndSaveAsync(user);
            var dateTime = DateTime.Now;
            user.LastLoginDate = dateTime;

            await unitOfWork.UpdateAndSaveAsync(user);

            user = await dbContext.Users.FindAsync("johndoe");
            Assert.AreEqual(dateTime, user.LastLoginDate);
        }

        [Test]
        public async Task ShouldGetEntityFromDbContext()
        {
            var dbContext = InfrastructureFactory.GetInMemoryContext(nameof(ShouldGetEntityFromDbContext));
            var unitOfWork = InfrastructureFactory.GetEfUnitOfWork(dbContext);
            var user = EntityFactory.GetUser("johndoe", "Pass@123");
            await unitOfWork.AddAndSaveAsync(user);

            user = await unitOfWork.GetSingleAsync<UserEntity>(u => u.Nickname == "johndoe");

            Assert.NotNull(user);
        }
    }
}
