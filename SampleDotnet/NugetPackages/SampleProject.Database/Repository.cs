using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SampleProject.Database.Interfaces.Repositories;
using System.Data;
using System.Linq.Expressions;

namespace SampleProject.Database
{
    public class Repository<TDbContext>
        : IRepository<TDbContext>, IDisposable
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private bool disposedValue;

        public DatabaseFacade Database { get => _context?.Database; }
        public ChangeTracker ChangeTracker { get => _context?.ChangeTracker; }

        public Repository(TDbContext dbContext)
        {
            _context = dbContext;
            _context.SavedChanges += _context_SavedChanges;
            _context.SaveChangesFailed += _context_SaveChangesFailed;
        }

        private void _context_SaveChangesFailed(object? sender, SaveChangesFailedEventArgs e)
        {
#if DEBUG
            Console.WriteLine($"Entities SaveChangesFailed: {e.Exception}");
#endif
        }

        private void _context_SavedChanges(object? sender, SavedChangesEventArgs e)
        {
#if DEBUG
            Console.WriteLine($"Entities SavedChanges: {e.EntitiesSavedCount}");
#endif
        }

        public IQueryable<T> AsQueryable<T>() where T : class
        {
            return _context.Set<T>().AsQueryable<T>();
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

        public T? Find<T>(params object[] keyValues) where T : class
        {
            return _context.Set<T>().Find(keyValues);
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
            _context.Set<T>().Add(entity);
        }

        public void Insert<T>(params T[] entities) where T : class
        {
            _context.Set<T>().AddRange(entities);
        }

        public void Insert<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().AddRange(entities);
        }

        public int SaveChanges()
        {
            var result = _context.SaveChanges();
            return result;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Attach(entity);
        }

        public void Update<T>(params T[] entities) where T : class
        {
            _context.AttachRange(entities);
        }

        public void Update<T>(IEnumerable<T> entities) where T : class
        {
            _context.AttachRange(entities);
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
                }

                disposedValue = true;
            }
        }
    }
}