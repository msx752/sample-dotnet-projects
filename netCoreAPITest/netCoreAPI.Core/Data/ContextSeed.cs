using netCoreAPI.Core.Interfaces;
using netCoreAPI.Core.Interfaces.Repositories.Shared;

namespace netCoreAPI.Core.Data
{
    public abstract class ContextSeed : IContextSeed
    {
        private bool initiated = false;

        public ContextSeed(ISharedConnection connection)
        {
            Connection = connection;
        }

        public ISharedConnection Connection { get; }

        public abstract void CommitSeed();

        public void Execute()
        {
            if (initiated)
                return;

            initiated = true;

            CommitSeed();
        }
    }
}