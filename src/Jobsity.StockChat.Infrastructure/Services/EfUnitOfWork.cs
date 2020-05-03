using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Infrastructure.Services
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly StockChatDbContext dbContext;

        public EfUnitOfWork(StockChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public Task AddAsync<T>(T entity) where T : class, IEntity, new()
        {
            return dbContext.AddAsync(entity).AsTask();
        }

        public async Task<IEnumerable<R>> FetchAsync<T, R>(Func<IQueryable<T>, IQueryable<R>> query)
            where T : class, IEntity, new()
            where R : new()
        {
            return await query(dbContext.Set<T>()).ToListAsync();
        }

        public Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity, new()
        {
            return dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public Task UpdateAsync<T>(T entity) where T : class, IEntity, new()
        {
            return Task.Run(() => dbContext.Update(entity));
        }

        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity, new()
        {
            return dbContext.Set<T>().AnyAsync(predicate);
        }
    }
}
