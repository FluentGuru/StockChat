using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Application.Entities;
using Jobsity.StockChat.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    internal static class StockChatDbContextExtensions
    {
        public static async Task<UserEntity> GetUser(this StockChatDbContext dbContext, string nickname, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Nickname == nickname, cancellationToken);
            if (user != null)
            {
                return user;
            }

            throw new UserNotFoundException($"'{nickname}' not found");
        }
    }
}
