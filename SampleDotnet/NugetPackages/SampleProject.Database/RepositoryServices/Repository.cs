using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Interfaces.Repositories;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace SampleProject.Core.RepositoryServices
{
    public class Repository<TDbContext>
        : IRepository<TDbContext>, IDisposable
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _dbsets = new ConcurrentDictionary<Type, object>();
        private bool disposedValue;

        public Repository(TDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool Any<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> query = AsQueryable<T>();
            return query.Any(predicate);
        }

        public IQueryable<T> AsNoTracking<T>() where T : class
        {
            IQueryable<T> query = AsQueryable<T>();
            return query.AsNoTracking();
        }

        public IQueryable<T> AsQueryable<T>() where T : class
        {
            return DbSet<T>().AsQueryable<T>();
        }

        public void Delete<T>(T entity) where T : class
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void Delete<T>(params T[] entities) where T : class
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public void Delete<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public bool Exists<T>(object id) where T : class
        {
            return GetById<T>(id) != null;
        }

        public T? Find<T>(params object[] keyValues) where T : class
        {
            return DbSet<T>().Find(keyValues);
        }

        public T? FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> query = AsQueryable<T>();
            return query.FirstOrDefault(predicate);
        }

        public T? GetById<T>(object id) where T : class
        {
            return Find<T>(id);
        }

        public void Insert<T>(T entity) where T : class
        {
            DbSet<T>().Attach(entity);
        }

        public void Insert<T>(params T[] entities) where T : class
        {
            DbSet<T>().AttachRange(entities);
        }

        public void Insert<T>(IEnumerable<T> entities) where T : class
        {
            DbSet<T>().AttachRange(entities);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Attach(entity);
        }

        public void Update<T>(params T[] entities) where T : class
        {
            foreach (var item in entities)
            {
                Update(item);
            }
        }

        public void Update<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var item in entities)
            {
                Update(item);
            }
        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return AsQueryable<T>().Where(predicate);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _dbsets.Clear();
                }

                disposedValue = true;
            }
        }

        private DbSet<T> DbSet<T>() where T : class
        {
            return (DbSet<T>)_dbsets.GetOrAdd(typeof(T), key => _context.Set<T>());
        }
    }
}