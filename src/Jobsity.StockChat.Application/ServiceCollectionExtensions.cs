using Jobsity.StockChat.Application.Services;
using Jobsity.StockChat.Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddServices();
            services.AddContext();
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

        private static void AddContext(this IServiceCollection services)
        {

        }
    }
}
