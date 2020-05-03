using Jobsity.StockChat.Application.Services;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Infrastructure.Data;
using Jobsity.StockChat.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddContext(
                configuration.GetValue<string>("Endpoint"),
                configuration.GetValue<string>("Subscription"));
            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDateTime, MachineDateTime>();
            services.AddSingleton<IHasher, Sha1Hasher>();
            services.AddTransient<IDataSource, EfUnitOfWork>();
            services.AddTransient<IPersistence, EfUnitOfWork>();
            services.AddTransient<IUnitOfWork, EfUnitOfWork>();
        }

        public static void AddContext(this IServiceCollection services, string endpoint, string subscription)
        {
            services.AddDbContextPool<StockChatDbContext>(options => options.UseCosmos(endpoint, subscription, "StockChat"));
        }

    }
}
