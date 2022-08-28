using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SampleProject.Core.Interfaces.Repositories
{
    public interface IEFRepository : IDisposable
    {
    }

    public interface IEFRepository<T>
        : IEFRepository
        where T : class
    {
        void Insert(T entity);

        void Insert(params T[] entities);

        void Insert(IEnumerable<T> entities);

        IQueryable<T> All(bool includeSoftDelete = false);

        bool Any(Expression<Func<T, bool>> predicate, bool includeSoftDelete = false);

        void Delete(T entity);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);

        T FirstOrDefault(Expression<Func<T, bool>> predicate, bool includeSoftDelete = false);

        bool Exists(object id, bool includeSoftDelete = false);

        T GetById(object id, bool includeSoftDelete = false);

        T Find(bool includeSoftDelete = false, params object[] keyValues);

        T Single(Expression<Func<T, T>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true, bool includeSoftDelete = false);

        void Update(T entity);

        void Update(params T[] entities);

        void Update(IEnumerable<T> entities);

        IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool includeSoftDelete = false);
    }
}