using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SampleProject.Core.Database;
using SampleProject.Core.Entities;
using SampleProject.Core.Interfaces.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SampleProject.Core.Interfaces.Repositories
{
    public interface IRepository<TDbContext> : IDisposable
        where TDbContext : DbContext
    {
        void Insert<T>(T entity) where T : class;

        void Insert<T>(params T[] entities) where T : class;

        void Insert<T>(IEnumerable<T> entities) where T : class;

        IQueryable<T> AsQueryable<T>() where T : class;

        IQueryable<T> AsNoTracking<T>() where T : class;

        bool Any<T>(Expression<Func<T, bool>> predicate) where T : class;

        void Delete<T>(T entity) where T : class;

        void Delete<T>(params T[] entities) where T : class;

        void Delete<T>(IEnumerable<T> entities) where T : class;

        T? FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;

        bool Exists<T>(object id) where T : class;

        T? GetById<T>(object id) where T : class;

        T? Find<T>(params object[] keyValues) where T : class;

        void Update<T>(T entity) where T : class;

        void Update<T>(params T[] entities) where T : class;

        void Update<T>(IEnumerable<T> entities) where T : class;

        IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;

        int SaveChanges();
    }
}