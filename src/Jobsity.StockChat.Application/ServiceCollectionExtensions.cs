using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Application.Services;
using Jobsity.StockChat.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddContext(
                configuration.GetValue<string>("Endpoint"), 
                configuration.GetValue<string>("Subscription"));
            services.AddMediator();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDateTime, MachineDateTime>();
            services.AddSingleton<IHasher, Sha1Hasher>();
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions));
        }

        private static void AddContext(this IServiceCollection services, string endpoint, string subscription)
        {
            services.AddDbContextPool<StockChatDbContext>(options => options.UseCosmos(endpoint, subscription, "StockChat"));
        }
    }
}
