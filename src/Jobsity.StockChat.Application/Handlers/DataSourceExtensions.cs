using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Exceptions;
using Jobsity.StockChat.Domain.Services;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    internal static class DataSourceExtensions
    {
        public static async Task<UserEntity> GetUser(this IDataSource dbContext, string nickname)
        {
            var user = await dbContext.GetSingleAsync<UserEntity>(u => u.Nickname == nickname);
            if (user != null)
            {
                return user;
            }

            throw new UserNotFoundException($"'{nickname}' not found");
        }
    }
}
