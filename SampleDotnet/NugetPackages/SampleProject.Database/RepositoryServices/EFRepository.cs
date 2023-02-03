using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SampleProject.Core.Database;
using SampleProject.Core.Entities;
using SampleProject.Core.Interfaces.Repositories;
using System.Linq.Expressions;

namespace SampleProject.Core.RepositoryServices
{
    internal sealed class EFRepository<T, TDbContext>
        : IEFRepository<T>, IDisposable
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
            _dbset.Attach(entity);
        }

        public void Insert(params T[] entities)
        {
            _dbset.AttachRange(entities);
        }

        public void Insert(IEnumerable<T> entities)
        {
            _dbset.AttachRange(entities);
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbset.AsQueryable<T>();
        }

        public IQueryable<T> AsNoTracking()
        {
            IQueryable<T> query = AsQueryable();
            return query.AsNoTracking();
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

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = AsQueryable();
            return query.FirstOrDefault(predicate);
        }

        public bool Exists(object id)
        {
            return GetById(id) != null;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = AsQueryable();
            return query.Any(predicate);
        }

        public T GetById(object id)
        {
            return Find(id);
        }

        public T Find(params object[] keyValues)
        {
            return _dbset.Find(keyValues);
        }

        public void Update(T entity)
        {
            _context.Attach(entity);
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}