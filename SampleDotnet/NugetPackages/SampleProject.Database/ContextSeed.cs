using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Interfaces.DbContexts;
using SampleProject.Core.Interfaces.Repositories;

namespace SampleProject.Core.Database
{
    public abstract class ContextSeed<TDbContext>
        : ContextSeed
        , IContextSeed<TDbContext>
        where TDbContext : DbContext
    {
        private bool initiated = false;

        public ContextSeed(IRepository<TDbContext> connection)
        {
            Repository = connection;
        }

        public IRepository<TDbContext> Repository { get; }

        public override sealed void Execute()
        {
            if (initiated)
                return;

            initiated = true;

            CommitSeed();
        }
    }

    public abstract class ContextSeed : IContextSeed
    {
        public abstract void CommitSeed();

        public abstract void Execute();
    }
}