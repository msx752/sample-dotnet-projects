using Microsoft.EntityFrameworkCore;
using netCoreAPI.Core.Interfaces.Repositories.Shared;

namespace netCoreAPI.Core.Interfaces
{
    public interface IContextSeed<TDbContext>
        : IContextSeed
        where TDbContext : DbContext
    {
        ISharedConnection<TDbContext> Connection { get; }
    }

    public interface IContextSeed
    {
        void Execute();
    }
}