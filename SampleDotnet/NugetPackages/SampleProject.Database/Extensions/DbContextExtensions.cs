using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Core.Database;
using SampleProject.Core.Entities;
using SampleProject.Core.Interfaces.DbContexts;
using SampleProject.Core.Interfaces.Repositories;
using SampleProject.Core.RepositoryServices;
using System;
using System.Linq;
using System.Reflection;

namespace SampleProject.Core.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// https://github.com/dotnet/efcore/blob/main/test/EFCore.Specification.Tests/TestUtilities/ServiceCollectionExtensions.cs#L8
        /// </summary>
        private static readonly MethodInfo _addDbContext
            = typeof(EntityFrameworkServiceCollectionExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(EntityFrameworkServiceCollectionExtensions.AddDbContext))
                .Single(
                    mi => mi.GetParameters().Length == 4
                        && mi.GetParameters()[1].ParameterType == typeof(Action<IServiceProvider, DbContextOptionsBuilder>)
                        && mi.GetGenericArguments().Length == 1);

        public static IServiceCollection AddCustomDbContext(
            this IServiceCollection services
            , params IDbContextParameter[] scopedDbContextParameters)
        {
            foreach (var dbContextParameter in scopedDbContextParameters)
            {
                ArgumentNullException.ThrowIfNull(dbContextParameter.DbContextType, nameof(dbContextParameter.DbContextType));

                #region DbContext Initializer

                AddDbContext(services, dbContextParameter.DbContextType, dbContextParameter.ActionDbContextOptionsBuilder);

                #endregion DbContext Initializer

                #region DbContext Repository Initializer

                var genericSharedRepository = typeof(Repository<>).MakeGenericType(dbContextParameter.DbContextType);
                var iGenericSharedRepository = typeof(IRepository<>).MakeGenericType(dbContextParameter.DbContextType);

                services.AddScoped(iGenericSharedRepository, genericSharedRepository);

                #endregion DbContext Repository Initializer

                #region DbContext Seed Initializer

                services.AddDbContextSeed(dbContextParameter.ContextSeedType);

                #endregion DbContext Seed Initializer
            }

            return services;
        }

        private static IServiceCollection AddDbContext(
            this IServiceCollection serviceCollection,
            Type contextType,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            => (IServiceCollection)_addDbContext.MakeGenericMethod(contextType)
                .Invoke(null, new object[] { serviceCollection, optionsAction, contextLifetime, optionsLifetime });

        public static IEnumerable<AuditEntry> DetectChanges(this DbContext dbContext)
        {
            if (!dbContext.ChangeTracker.AutoDetectChangesEnabled)
                dbContext.ChangeTracker.DetectChanges();

            foreach (var entry in dbContext.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry.Entity.GetType().Name, entry.State);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.PrimaryKeys[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (auditEntry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified && property.CurrentValue?.ToString() != property.OriginalValue?.ToString())
                            {
                                auditEntry.AffectedColumns.Add(propertyName);
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }

                yield return auditEntry;
            }
        }
    }
}