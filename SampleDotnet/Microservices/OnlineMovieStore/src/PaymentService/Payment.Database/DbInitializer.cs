using Microsoft.EntityFrameworkCore;

namespace Payment.Database
{
    public static class DbInitializer
    {
        public static void Initialize(PaymentDbContext context)
        {
            using (context)
            {
                context.Database.EnsureCreated();
                context.SaveChanges();
            }

        }
    }
}