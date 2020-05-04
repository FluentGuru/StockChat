using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Jobsity.StockChat.Client
{
    public static class ServiceCollectionExtensions
    {
        public static void AddStockChatClient(this IServiceCollection services, string baseAddress = "/api")
        {
            services.AddHttpClient<StockChatClient>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            });
        }
    }
}
