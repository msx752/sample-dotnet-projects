using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SampleProject.Core.Database;
using SampleProject.Core.Entities;
using SampleProject.Core.Extensions;
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

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
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