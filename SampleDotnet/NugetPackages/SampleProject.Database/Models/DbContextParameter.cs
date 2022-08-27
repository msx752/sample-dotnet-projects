using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Interfaces.DbContexts;
using System;

namespace SampleProject.Core.Model
{
    public class DbContextParameter<TMyContext, TContextSeed>
        : DbContextParameter<TMyContext>
        where TMyContext : DbContext
        where TContextSeed : IContextSeed
    {
        public DbContextParameter(Action<IServiceProvider, DbContextOptionsBuilder> actionDbContextOptionsBuilder = null)
            : base(actionDbContextOptionsBuilder)
        {
        }

        public override Type ContextSeedType { get => typeof(TContextSeed); }
    }

    public class DbContextParameter<TMyContext>
        : IDbContextParameter
        where TMyContext : DbContext
    {
        public DbContextParameter(Action<IServiceProvider, DbContextOptionsBuilder> actionDbContextOptionsBuilder = null)
        {
            ActionDbContextOptionsBuilder = actionDbContextOptionsBuilder;
        }

        public virtual Type ContextSeedType { get; }
        public Action<IServiceProvider, DbContextOptionsBuilder> ActionDbContextOptionsBuilder { get; }
        public Type DbContextType { get => typeof(TMyContext); }
    }
}