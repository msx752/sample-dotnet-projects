using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace netCoreAPI.Core.Interfaces.Repositories
{
    public interface IEntityRepository : IDisposable
    {
    }

    public interface IEntityRepository<T>
        : IEntityRepository
        where T : class
    {
        T Add(T entity);

        void Add(params T[] entities);

        void Add(IEnumerable<T> entities);

        IQueryable<T> All();

        T Delete(T entity);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        T GetById(object id);

        T Search(params object[] keyValues);

        T Single(Expression<Func<T, T>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);

        T Update(T entity);

        void Update(params T[] entities);

        void Update(IEnumerable<T> entities);

        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);
    }
}