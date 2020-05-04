using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Domain.Services
{
    public interface IChatNotifier
    {
        Task NotifyAsync<T>(string group, string action, T data);
    }
}
