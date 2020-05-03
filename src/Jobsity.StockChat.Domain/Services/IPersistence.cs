using Jobsity.StockChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Domain.Services
{
    public interface IPersistence
    {
        Task AddAsync<T>(T entity) where T : class, IEntity, new();
        Task UpdateAsync<T>(T entity) where T : class, IEntity, new();
        Task SaveChangesAsync();
    }

    public static class PersistenceExtensions
    {
        public async static Task AddAndSaveAsync<T>(this IPersistence persistence, T entity) where T : class, IEntity, new()
        {
            await persistence.AddAsync(entity);
            await persistence.SaveChangesAsync();
        }

        public async static Task UpdateAndSaveAsync<T>(this IPersistence persistence, T entity) where T : class, IEntity, new()
        {
            await persistence.UpdateAsync(entity);
            await persistence.SaveChangesAsync();
        }
    }
}
