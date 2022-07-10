using Microsoft.EntityFrameworkCore;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Interfaces.Repositories.Shared;
using System;
using System.Collections.Concurrent;

namespace Samp.Core.RepositoryServices
{
    public sealed class SharedRepository<TDbContext>
        : ISharedRepository<TDbContext>
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        private bool _disposed;

        public SharedRepository(TDbContext context)
        {
            _repositories = new ConcurrentDictionary<Type, object>();
            _context = context;
        }

        public IEntityRepository<TEntity> Db<TEntity>() where TEntity : class
        {
            return _repositories.GetOrAdd(typeof(TEntity), new EntityRepository<TEntity, TDbContext>(_context)) as IEntityRepository<TEntity>;
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

        /// <summary>
        /// MAGIC
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
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