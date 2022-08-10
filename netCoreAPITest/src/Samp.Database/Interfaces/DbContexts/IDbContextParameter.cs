using Microsoft.EntityFrameworkCore;
using System;

namespace Samp.Core.Interfaces.DbContexts
{
    public interface IDbContextParameter
    {
        public Action<IServiceProvider, DbContextOptionsBuilder> ActionDbContextOptionsBuilder { get; }
        Type ContextSeedType { get; }
        Type DbContextType { get; }
    }
}