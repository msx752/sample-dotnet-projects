using netCoreAPI.Core.Interfaces.Repositories;

namespace netCoreAPI.Core.Interfaces.Repositories.Shared
{
    public interface ISharedRepository
    {
        int SaveChanges();

        IEntityRepository<TEntity> Db<TEntity>() where TEntity : class;

        void Dispose();
    }
}