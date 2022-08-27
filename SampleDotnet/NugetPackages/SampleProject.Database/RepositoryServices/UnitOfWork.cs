using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Database;
using SampleProject.Core.Entities;
using SampleProject.Core.Interfaces.Repositories;
using System;
using System.Collections.Concurrent;

namespace SampleProject.Core.RepositoryServices
{
    public sealed class UnitOfWork<TDbContext>
        : IUnitOfWork<TDbContext>
        where TDbContext : SampBaseContext
    {
        private readonly TDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        private bool _disposed;

        public UnitOfWork(TDbContext context)
        {
            _repositories = new ConcurrentDictionary<Type, object>();
            _context = context;
        }

        /// <summary>
        /// MAGIC
        /// </summary>
        /// <returns></returns>
        public int SaveChanges(Guid userId)
        {
            return _context.SaveChanges(userId);
        }

        public IEFRepository<TEntity> Table<TEntity>()
                    where TEntity : BaseEntity
        {
            var lazy = _repositories
                .GetOrAdd(typeof(TEntity), new Lazy<EFRepository<TEntity, TDbContext>>(() => new EFRepository<TEntity, TDbContext>(_context)));

            return ((Lazy<EFRepository<TEntity, TDbContext>>)lazy).Value;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int RawQuery(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(sql, parameters);
        }

        internal void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                    _repositories?.Clear();
                }
                _disposed = true;
            }
        }
    }
}