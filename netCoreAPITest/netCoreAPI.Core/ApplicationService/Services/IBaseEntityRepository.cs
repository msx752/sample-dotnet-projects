using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace netCoreAPI.Core.ApplicationService.Services
{
    public interface IBaseEntityRepository<T> : IDisposable where T : class
    {
        T Search(params object[] keyValues);

        T Single(Expression<Func<T, T>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);

        EntityEntry<T> Add(T entity);

        void Add(params T[] entities);

        void Add(IEnumerable<T> entities);

        EntityEntry<T> Delete(T entity);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);

        EntityEntry<T> Update(T entity);

        void Update(params T[] entities);

        void Update(IEnumerable<T> entities);

        T GetById(object id);

        IQueryable<T> All();

        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);
    }
}