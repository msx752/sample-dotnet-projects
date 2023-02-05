using Microsoft.EntityFrameworkCore;

namespace Cart.Database
{
    public static class DbInitializer
    {
        public static void Initialize(IDbContextFactory<CartDbContext> contextFactory)
        {
            using (var context = contextFactory.CreateRepository())
            {
                context.Database.EnsureCreated();

                context.SaveChanges();
            }
        }
    }
}