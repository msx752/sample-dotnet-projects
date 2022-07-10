using Microsoft.EntityFrameworkCore;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Interfaces.Repositories.Shared;
using System;

namespace Samp.Core.RepositoryServices
{
    public sealed class SharedRepository<TDbContext>
        : ISharedRepository<TDbContext>
        where TDbContext : DbContext
    {
        private readonly ISharedConnection<TDbContext> _uow;

        private bool _disposed;

        public SharedRepository(ISharedConnection<TDbContext> uow)
        {
            _uow = uow;
        }

        public IEntityRepository<TEntity> Db<TEntity>() where TEntity : class
        {
            return _uow.Db<TEntity>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return _uow.SaveChanges();
        }

        internal void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _uow?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}