using Microsoft.EntityFrameworkCore;
using netCoreAPI.Core.Interfaces;
using netCoreAPI.Core.Interfaces.Repositories.Shared;

namespace netCoreAPI.Core.Data
{
    public abstract class ContextSeed<TDbContext>
        : ContextSeed
        , IContextSeed<TDbContext>
        where TDbContext : DbContext
    {
        private bool initiated = false;

        public ContextSeed(ISharedConnection<TDbContext> connection)
        {
            Connection = connection;
        }

        public ISharedConnection<TDbContext> Connection { get; }

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