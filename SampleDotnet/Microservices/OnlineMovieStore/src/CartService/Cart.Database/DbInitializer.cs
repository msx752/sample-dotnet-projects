using Microsoft.EntityFrameworkCore;


namespace Cart.Database
{
    public static class DbInitializer
    {
        public static void Initialize(CartDbContext context)
        {
            using (context)
            {
                context.Database.EnsureCreated();
                context.SaveChanges();
            }
        }
    }
}