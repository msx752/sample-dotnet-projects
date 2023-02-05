using Microsoft.EntityFrameworkCore;

namespace Payment.Database
{
    public static class DbInitializer
    {
        public static void Initialize(IDbContextFactory<PaymentDbContext> contextFactory)
        {
            using (var context = contextFactory.CreateRepository())
            {
                context.Database.EnsureCreated();

                context.SaveChanges();
            }
        }
    }
}