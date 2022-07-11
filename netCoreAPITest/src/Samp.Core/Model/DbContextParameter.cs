using Microsoft.EntityFrameworkCore;
using Samp.Core.Interfaces;
using System;

namespace Samp.Core.Model
{
    public class DbContextParameter<TMyContext, TContextSeed>
        : IDbContextParameter
        where TMyContext : DbContext
        where TContextSeed : IContextSeed
    {
        public DbContextParameter(Action<IServiceProvider, DbContextOptionsBuilder> actionDbContextOptionsBuilder)
            : this()
        {
            if (actionDbContextOptionsBuilder != null)
            {
                ActionDbContextOptionsBuilder = actionDbContextOptionsBuilder;
            }
        }

        public DbContextParameter()
        {
        }

        public Action<IServiceProvider, DbContextOptionsBuilder> ActionDbContextOptionsBuilder { get; }
        public Type ContextSeed { get => typeof(TContextSeed); }
        public Type DbContext { get => typeof(TMyContext); }
    }
}