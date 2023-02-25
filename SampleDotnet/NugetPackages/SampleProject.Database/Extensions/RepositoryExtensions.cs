public static class RepositoryExtensions
{
    public static IRepository<TDbContext> CreateRepository<TDbContext>(this IDbContextFactory<TDbContext> contextFactory)
        where TDbContext : DbContext
    {
        //https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/#avoiding-dbcontext-threading-issues
        var dbContext = contextFactory.CreateDbContext();
        return new Repository<TDbContext>(dbContext);
    }
}