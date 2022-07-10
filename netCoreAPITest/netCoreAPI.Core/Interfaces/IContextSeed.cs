using netCoreAPI.Core.Interfaces.Repositories.Shared;

namespace netCoreAPI.Core.Interfaces
{
    public interface IContextSeed
    {
        ISharedConnection Connection { get; }

        void Execute();
    }
}