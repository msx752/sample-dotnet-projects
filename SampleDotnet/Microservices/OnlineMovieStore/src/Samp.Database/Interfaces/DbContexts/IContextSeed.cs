using Microsoft.EntityFrameworkCore;
using Samp.Core.Interfaces.Repositories;

namespace Samp.Core.Interfaces.DbContexts
{
    public interface IContextSeed<TDbContext>
        : IContextSeed
        where TDbContext : DbContext
    {
        IUnitOfWork<TDbContext> Repository { get; }
    }

    public interface IContextSeed
    {
        void Execute();
    }
}