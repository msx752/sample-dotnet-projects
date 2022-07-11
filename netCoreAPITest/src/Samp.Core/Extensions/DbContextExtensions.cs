using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Samp.Core.Extensions
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
            this IServiceCollection serviceCollection,
            Type contextType,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            => (IServiceCollection)_addDbContext.MakeGenericMethod(contextType)
                .Invoke(null, new object[] { serviceCollection, optionsAction, contextLifetime, optionsLifetime });
    }
}