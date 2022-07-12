using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Samp.Core.Database;
using Samp.Core.Entities;
using Samp.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Samp.Core.RepositoryServices
{
    internal sealed class EFRepository<T, TDbContext>
        : IEFRepository<T>
        where T : BaseEntity
        where TDbContext : SampBaseContext
    {
        private readonly TDbContext _context;
        private readonly DbSet<T> _dbset;

        public EFRepository(TDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public T Add(T entity)
        {
            entity.IsActive = true;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return _dbset.Add(entity).Entity;
        }

        public void Add(params T[] entities)
        {
            foreach (var entity in entities)
            {
                entity.IsActive = true;
                entity.CreatedAt = DateTimeOffset.UtcNow;
            }
            _dbset.AddRange(entities);
        }

        public void Add(IEnumerable<T> entities)
        {
            _dbset.AddRange(entities);
        }

        public IQueryable<T> All(bool includesInActives = false)
        {
            var query = _dbset
                .AsQueryable<T>()
                .Where(f => f.IsActive == !includesInActives);
            return query;
        }

        public T Delete(T entity)
        {
            entity.IsActive = false;
            return Update(entity);
            //var entry = _context.Entry(entity);
            //entry.State = EntityState.Deleted;
            //return entry.Entity;
        }

        public void Delete(params T[] entities)
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate, bool includesInActives = false)
        {
            return All(includesInActives).FirstOrDefault(predicate);
        }

        public T GetById(object id, bool includesInActives = false)
        {
            var entity = _dbset.Find(id);

            if (entity == null || !entity.IsActive)
                return null;

            return entity;
        }

        public T Search(bool includesInActives = false, params object[] keyValues)
        {
            var entity = _dbset.Find(keyValues);

            if (entity == null || !entity.IsActive)
                return null;

            return entity;
        }

        /// <summary>
        /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
        /// https://entityframeworkcore.com/knowledge-base/46374252/how-to-write-repository-method-for--theninclude-in-ef-core-2
        /// </summary>
        /// <param name="selector">The selector for projection.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method default no-tracking query.</remarks>
        public T Single(Expression<Func<T, T>> selector,
                                    Expression<Func<T, bool>> predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                    bool disableTracking = true,
                                    bool includesInActives = false)
        {
            IQueryable<T> query = All(includesInActives);
            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).Select(selector).FirstOrDefault();
            else
                return query.Select(selector).FirstOrDefault();
            /*
             * EXAMPLE USAGE
             * var affiliate = await affiliateRepository.GetFirstOrDefaultAsync(
        predicate: b => b.Id == id,
        include: source => source
            .Include(a => a.Branches)
            .ThenInclude(a => a.Emails)
            .Include(a => a.Branches)
            .ThenInclude(a => a.Phones));
             */
        }

        public T Update(T entity)
        {
            var entry = _context.Entry(entity);
            //_dbset.Attach(entity);
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entry.State = EntityState.Modified;
            return entry.Entity;
        }

        public void Update(params T[] entities)
        {
            foreach (var item in entities)
            {
                Update(item);
            }
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                Update(item);
            }
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate, bool includesInActives = false)
        {
            return All(includesInActives).Where(predicate).AsEnumerable();
        }
    }
}