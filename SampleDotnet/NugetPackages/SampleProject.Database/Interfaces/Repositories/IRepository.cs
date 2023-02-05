using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SampleProject.Core.Interfaces.DbContexts;
using System.Linq.Expressions;

namespace SampleProject.Core.Interfaces.Repositories
{
    public interface IRepository<TDbContext> : IDisposable
        where TDbContext : DbContext
    {
        DatabaseFacade Database { get; }

        IQueryable<T> AsQueryable<T>() where T : class;

        void Delete<T>(T entity) where T : class;

        void Delete<T>(params T[] entities) where T : class;

        void Delete<T>(IEnumerable<T> entities) where T : class;

        T? Find<T>(params object[] keyValues) where T : class;

        T? FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;

        T? GetById<T>(object id) where T : class;

        void Insert<T>(T entity) where T : class;

        void Insert<T>(params T[] entities) where T : class;

        void Insert<T>(IEnumerable<T> entities) where T : class;

        int SaveChanges();

        void Update<T>(T entity) where T : class, IBaseEntity;

        void Update<T>(params T[] entities) where T : class, IBaseEntity;

        void Update<T>(IEnumerable<T> entities) where T : class, IBaseEntity;

        IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
    }
}