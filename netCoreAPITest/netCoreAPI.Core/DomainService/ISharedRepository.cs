using netCoreAPI.Core.ApplicationService.Services;

namespace netCoreAPI.Static.Services
{
    public interface ISharedRepository
    {
        int SaveChanges();

        IBaseEntityRepository<TEntity> Db<TEntity>() where TEntity : class;

        void Dispose();
    }
}