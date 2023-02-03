using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Interfaces.Repositories;

namespace SampleProject.Core.Interfaces.DbContexts
{
    public interface IContextSeed<TDbContext>
        : IContextSeed
        where TDbContext : DbContext
    {
        IRepository<TDbContext> Repository { get; }
    }

    public interface IContextSeed
    {
        void Execute();
    }
}