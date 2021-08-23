using netCoreAPI.Core.ApplicationService;
using netCoreAPI.Core.ApplicationService.Services;
using netCoreAPI.Core.DomainService.SubRepositories;
using System;

namespace netCoreAPI.Static.Services
{
    public partial class MyRepository : IMyRepository, IDisposable
    {
        private readonly IUnitOfWork _uow;

        private bool _disposed;
        private PersonalRepo _personalRepo;

        public MyRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public PersonalRepo PersonalRepo
        {
            get
            {
                if (_personalRepo == null)
                    _personalRepo = new PersonalRepo(_uow);
                return _personalRepo;
            }
        }

        public int SaveChanges()
        {
            return _uow.SaveChanges();
        }

        public IEFRepository<TEntity> Db<TEntity>() where TEntity : class
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
                    _personalRepo = null;
                }
                this._disposed = true;
            }
        }
    }
}