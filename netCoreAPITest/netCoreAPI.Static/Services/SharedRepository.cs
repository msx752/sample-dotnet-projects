﻿using netCoreAPI.Core.ApplicationService;
using netCoreAPI.Core.ApplicationService.Services;
using System;

namespace netCoreAPI.Static.Services
{
    public partial class SharedRepository : ISharedRepository, IDisposable
    {
        private readonly ISharedConnection _uow;

        private bool _disposed;

        public SharedRepository(ISharedConnection uow)
        {
            _uow = uow;
        }

        public int SaveChanges()
        {
            return _uow.SaveChanges();
        }

        public IBaseEntityRepository<TEntity> Db<TEntity>() where TEntity : class
        {
            return _uow.Db<TEntity>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
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