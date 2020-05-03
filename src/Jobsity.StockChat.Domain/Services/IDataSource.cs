using Jobsity.StockChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Domain.Services
{
    public interface IDataSource
    {
        Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity, new();
        Task<IEnumerable<R>> FetchAsync<T, R>(Func<IQueryable<T>, IQueryable<R>> query)
            where T : class, IEntity, new()
            where R : new();
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity, new();
    }
}
