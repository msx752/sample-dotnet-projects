using netCoreAPI.Core.ApplicationService.Services;
using netCoreAPI.Core.DomainService.SubRepositories;

namespace netCoreAPI.Static.Services
{
    public interface IMyRepository
    {
        PersonalRepo PersonalRepo { get; }

        int Commit();

        IEFRepository<TEntity> Db<TEntity>() where TEntity : class;

        void Dispose();
    }
}