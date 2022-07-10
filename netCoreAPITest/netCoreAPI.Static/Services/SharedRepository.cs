using Microsoft.EntityFrameworkCore;
using netCoreAPI.Core.Interfaces.Repositories;
using netCoreAPI.Core.Interfaces.Repositories.Shared;
using System;

namespace netCoreAPI.Static.Services
{
    public sealed class SharedRepository<TDbContext>
        : ISharedRepository
        , IDisposable
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
            if (!this._disposed)
            {
                if (disposing)
                {
                    _uow?.Dispose();
                }
                this._disposed = true;
            }
        }
    }
}