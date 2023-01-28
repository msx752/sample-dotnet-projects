using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SampleProject.Core.Database;
using SampleProject.Core.Entities;
using SampleProject.Core.Interfaces.Repositories;
using System.Linq.Expressions;

namespace SampleProject.Core.RepositoryServices
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

        public void Insert(T entity)
        {
            _dbset.Add(entity);
        }

        public void Insert(params T[] entities)
        {
            _dbset.AddRange(entities);
        }

        public void Insert(IEnumerable<T> entities)
        {
            _dbset.AddRange(entities);
        }

        public IQueryable<T> AsQueryable()
        {
            IQueryable<T> queryable = _dbset.AsQueryable<T>();
            return queryable;
        }

        public void Delete(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;
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

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return AsQueryable().FirstOrDefault(predicate);
        }

        public bool Exists(object id)
        {
            return GetById(id) != null;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return AsQueryable().Any(predicate);
        }

        public T GetById(object id)
        {
            return Find(id);
        }

        public T Find(params object[] keyValues)
        {
            var entity = _dbset.Find(keyValues);

            if (entity == null)
            {
                return null;
            }

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
                                    bool disableTracking = true)
        {
            IQueryable<T> query = AsQueryable();
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

        public IQueryable<T> AsNoTracking()
        {
            IQueryable<T> query = _dbset.AsQueryable<T>();
            return query.AsNoTracking();
        }

        public void Update(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
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

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return AsQueryable().Where(predicate);
        }
    }
}