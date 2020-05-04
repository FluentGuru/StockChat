using Jobsity.StockChat.Application.Services;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Infrastructure.Data;
using Jobsity.StockChat.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.UnitTests.Commons
{
    public static class InfrastructureFactory
    {
        public static StockChatDbContext GetInMemoryContext(string databaseName = "StockChat")
        {
            var options = new DbContextOptionsBuilder<StockChatDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
            return new StockChatDbContext(options);
        }

        public static EfUnitOfWork GetEfUnitOfWork(StockChatDbContext dbContext)
            => new EfUnitOfWork(dbContext);

        public static EfUnitOfWork GetInMemoryUnitOfWork(string databaseName = "StockChat")
            => GetEfUnitOfWork(GetInMemoryContext(databaseName));

        public static IDateTime GetMachineDateTime() => new MachineDateTime();

        public static IHasher GetSha1Hasher() => new Sha1Hasher();

        public static IChatNotifier GetNotifierSubstitute() => Substitute.For<IChatNotifier>();

        public static IMediator GetMediatorSubstitute() => Substitute.For<IMediator>();
    }
}
