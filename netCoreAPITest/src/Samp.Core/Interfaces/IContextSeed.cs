using Microsoft.EntityFrameworkCore;
using Samp.Core.Interfaces.Repositories.Shared;

namespace Samp.Core.Interfaces
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